using System.Collections.Generic;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Constant;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Parser.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Util;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Parser
{
    public class SelectCounterParser : BaseParser
    {
        protected override IOcgMessage DoParse(byte[] buffer)
        {
            var reader = new PacketReader(buffer);
            reader.ReadByte(); //msg
            var player = reader.ReadByte();
            var counterType = (OCG_CounterType)reader.ReadUInt16();
            var counterAmount = reader.ReadUInt16();
            var count = reader.ReadUInt32();
            var cards = new List<CardReference>();

            for (var i = count; i > 0; i--)
            {
                var cardCode = reader.ReadUInt32();
                var controller = reader.ReadByte();
                var location = (OCG_CardLocation)reader.ReadByte();
                var sequence = reader.ReadByte();
                var cardCounter = reader.ReadUInt16();
                var card = new CardReference(cardCode, new FullLocationReference(controller, location, sequence, 0),
                    count - i);
                card.CounterAmount = cardCounter;
            }

            return new SelectCounterMessage(player, counterType, counterAmount, cards);
        }
    }
}