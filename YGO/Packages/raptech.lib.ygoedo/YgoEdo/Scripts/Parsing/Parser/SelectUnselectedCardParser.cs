using System.Collections.Generic;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Parser.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Util;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Parser
{
    public class SelectUnselectedCardParser : BaseParser
    {
        protected override IOcgMessage DoParse(byte[] buffer)
        {
            var reader = new PacketReader(buffer);
            reader.ReadByte(); //msg
            var player = reader.ReadByte();
            var finishable = reader.ReadByte() == 1;
            var cancelable = reader.ReadByte() == 1;
            var min = reader.ReadUInt32();
            var max = reader.ReadUInt32();
            var cardsToSelectSize = reader.ReadUInt32();

            var cardsToSelect = new List<CardReference>();
            for (var i = cardsToSelectSize; i > 0; i--)
            {
                var cardCode = reader.ReadUInt32();
                var controller = reader.ReadByte();
                var location = (OCG_CardLocation)reader.ReadByte();
                var sequence = reader.ReadUInt32();
                var position = (OCG_CardPosition)reader.ReadUInt32();
                cardsToSelect.Add(new CardReference(cardCode,
                    new FullLocationReference(controller, location, sequence, position), cardsToSelectSize - i));
            }

            var cardsToUnselectSize = reader.ReadUInt32();
            var cardsToUnselect = new List<CardReference>();

            for (var i = cardsToUnselectSize; i > 0; i--)
            {
                var cardCode = reader.ReadUInt32();
                var controller = reader.ReadByte();
                var location = (OCG_CardLocation)reader.ReadByte();
                var sequence = reader.ReadUInt32();
                var position = (OCG_CardPosition)reader.ReadUInt32();
                cardsToUnselect.Add(new CardReference(cardCode,
                    new FullLocationReference(controller, location, sequence, position), cardsToUnselectSize - i));
            }

            return new SelectUnselectedCardMessage(player, finishable, cancelable, min, max, cardsToSelect,
                cardsToUnselect);
        }
    }
}