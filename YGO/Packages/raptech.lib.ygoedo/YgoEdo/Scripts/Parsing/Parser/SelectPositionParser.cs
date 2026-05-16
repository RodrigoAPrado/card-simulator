using System;
using System.Collections.Generic;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Parser.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Util;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Parser
{
    public class SelectPositionParser : BaseParser
    {
        protected override IOcgMessage DoParse(byte[] buffer)
        {
            var reader = new PacketReader(buffer);
            reader.ReadByte(); //msg
            var player = reader.ReadByte();
            var cardCode = reader.ReadUInt32();
            var mask = reader.ReadByte();
            var availablePositions = new List<OCG_CardPosition>();

            foreach (OCG_CardPosition pos in Enum.GetValues(typeof(OCG_CardPosition)))
                switch (pos)
                {
                    case OCG_CardPosition.FaceUpAttack:
                    case OCG_CardPosition.FaceDownAttack:
                    case OCG_CardPosition.FaceUpDefense:
                    case OCG_CardPosition.FaceDownDefense:
                        if ((mask & (byte)pos) != 0)
                            availablePositions.Add(pos);
                        break;
                    default:
                        continue;
                }

            return new SelectPositionMessage(player, cardCode, availablePositions);
        }
    }
}