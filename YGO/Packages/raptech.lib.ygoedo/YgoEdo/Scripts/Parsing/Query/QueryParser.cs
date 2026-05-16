using System.Collections.Generic;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Query.Component;
using YgoSoul.RapTech.Lib.YgoEdo.Util;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Query
{
    public class QueryParser
    {
        public static FieldQuery ParseField(byte[] buffer)
        {
            var reader = new PacketReader(buffer);
            reader.ReadUInt32(); //
            var fieldBuilder = FieldQuery.Builder.Create();
            for (byte i = 0; i < 2; i++) // player info
            {
                var lp = reader.ReadUInt32();
                var infos = new Dictionary<int, FieldQueryCard>();
                for (var j = 0; j < ZoneUtils.ZoneIndexQuery.Count; j++)
                {
                    var hasCard = reader.ReadByte() == 1;
                    if (hasCard)
                        infos.Add(j,
                            new FieldQueryCard(ZoneUtils.ZoneIndexQuery[j], (OCG_CardPosition)reader.ReadByte(),
                                reader.ReadUInt32()));
                }

                fieldBuilder.AddPlayerInfo(i, new PlayerInfo(
                    lp,
                    infos,
                    reader.ReadUInt32(),
                    reader.ReadUInt32(),
                    reader.ReadUInt32(),
                    reader.ReadUInt32(),
                    reader.ReadUInt32(),
                    reader.ReadUInt32()));
            }

            var chainSize = reader.ReadUInt32();
            var chainList = new List<FieldQueryChain>();
            for (var i = chainSize; i > 0; i--)
                chainList.Add(new FieldQueryChain(
                    reader.ReadUInt32(),
                    new FullLocationReference(
                        reader.ReadByte(),
                        (OCG_CardLocation)reader.ReadByte(),
                        reader.ReadUInt32(),
                        (OCG_CardPosition)reader.ReadUInt32()
                    ),
                    reader.ReadByte(),
                    (OCG_CardLocation)reader.ReadByte(),
                    reader.ReadUInt32(),
                    reader.ReadULong64()
                ));
            fieldBuilder.AddFieldQueryChain(chainList);

            return fieldBuilder.Build();
        }
    }
/*
 * 00-E8-02-00-
 * 40-1F-00-00- LP Player 0
 * 00- // Mzone 0-> 0 = nada ali, 1 = carta ali, e ai tem 8 bits com a posição e 32 c quantidade de xyz material
 * 00- // mzone 1
 * 00- // mzone 2
 * 00- // mzone 3
 * 00- // mzone 4
 * 00- // extra 0
 * 00- // extra 1
 * 00- // szone 0
 * 00- // szone 1
 * 00- // szone 2
 * 00- // szone 3
 * 00- // szone 4
 * 00- // field
 * 00- // pendulum left
 * 00- // pendulum right
 * 23-00-00-00- Main Deck Size (35)
 * 05-00-00-00- Hand Size (5)
 * 00-00-00-00- Grave Size (0)
 * 00-00-00-00- Remove Size (0)
 * 0C-00-00-00- Extra Size (12)
 * 00-00-00-00- Extra count?
 * 40-1F-00-00- LP Player 1
 * 00- // Mzone 0-> 0 = nada ali, 1 = carta ali, e ai tem 8 bits com a posição e 32 c quantidade de xyz material
 * 00- // mzone 1
 * 00- // mzone 2
 * 00- // mzone 3
 * 00- // mzone 4
 * 00- // extra 0
 * 00- // extra 1
 * 00- // szone 0
 * 00- // szone 1
 * 00- // szone 2
 * 00- // szone 3
 * 00- // szone 4
 * 00- // field
 * 00- // pendulum left
 * 00- // pendulum right
 * 23-00-00-00- // main size
 * 05-00-00-00- // hand size
 * 00-00-00-00- // grave size
 * 00-00-00-00- // remove
 * 0C-00-00-00- // extra size
 * 00-00-00-00- // extra count?
 * 00-00-00-00 // current chain size
 * xx-xx-xx-xx // card code
 * xx // controller
 * xx // location
 * xx-xx-xx-xx // sequence
 * xx-xx-xx-xx // position
 * xx // triggering controller
 * xx // triggering location
 * xx-xx-xx-xx // triggering sequence
 * xx-xx-xx-xx-xx-xx-xx-xx // description
 */
}