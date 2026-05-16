using System.Collections.Generic;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Parser.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Util;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Parser
{
    public class SelectSumParser : BaseParser
    {
        protected override IOcgMessage DoParse(byte[] buffer)
        {
            var reader = new PacketReader(buffer);
            reader.ReadByte(); //msg
            var player = reader.ReadByte();
            var hasMax = reader.ReadByte() == 0;
            var acc = reader.ReadUInt32();
            var min = reader.ReadUInt32();
            var max = reader.ReadUInt32();
            var mustSelectCount = reader.ReadUInt32();
            var mustSelectCards = new List<CardReference>();

            uint index = 0;

            for (var i = mustSelectCount; i > 0; i--)
            {
                var cardCode = reader.ReadUInt32();
                var controller = reader.ReadByte();
                var location = (OCG_CardLocation)reader.ReadByte();
                var sequence = reader.ReadUInt32();
                var position = (OCG_CardPosition)reader.ReadUInt32();
                var card = new CardReference(cardCode,
                    new FullLocationReference(controller, location, sequence, position), index);
                card.Sum = reader.ReadUInt32();
                mustSelectCards.Add(card);
                index++;
            }

            var count = reader.ReadUInt32();
            var canSelect = new List<CardReference>();

            for (var i = count; i > 0; i--)
            {
                var cardCode = reader.ReadUInt32();
                var controller = reader.ReadByte();
                var location = (OCG_CardLocation)reader.ReadByte();
                var sequence = reader.ReadUInt32();
                var position = (OCG_CardPosition)reader.ReadUInt32();
                var card = new CardReference(cardCode,
                    new FullLocationReference(controller, location, sequence, position), index);
                card.Sum = reader.ReadUInt32();
                canSelect.Add(card);
                index++;
            }

            return new SelectSumMessage(player, hasMax, acc, min, max, mustSelectCards, canSelect);
        }
    }

/*
 *

17- msg
00- // PlayerId
01- // Se tiver um valor máximo, ou seja, tem que bater
08-00-00-00- // acc
00-00-00-00- // min
00-00-00-00- // max
00-00-00-00- // must select size
03-00-00-00- // can select size
48-7F-26-03- // code
00- // Controller
02- // Location
01-00-00-00- // Sequence
0A-00-00-00- // Position
04-00-00-00- // Sum Value
A3-32-31-01- // code
00- // Controller
02- // Location
02-00-00-00- // Sequence
0A-00-00-00- // Position
04-00-00-00- // Sum Value
A8-8E-92-05- // code
00- // Controlle
02- // Location
03-00-00-00- // Sequence
0A-00-00-00- // Position
06-00-00-00 // Sum Value

 *
 */
}