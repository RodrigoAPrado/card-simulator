using System;
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
    public class ConfirmCardParser : BaseParser
    {
        protected override IOcgMessage DoParse(byte[] buffer)
        {
            var reader = new PacketReader(buffer);
            var msg = (OCG_GameMessage)reader.ReadByte();
            var player = reader.ReadByte();
            var count = reader.ReadUInt32();

            var cards = new List<CardReference>();

            for (var i = count; i > 0; i--)
            {
                var cardCode = reader.ReadUInt32();
                var controller = reader.ReadByte();
                var location = (OCG_CardLocation)reader.ReadByte();
                var sequence = reader.ReadUInt32();
                cards.Add(new CardReference(cardCode, new FullLocationReference(controller, location, sequence, 0),
                    count - i));
            }

            switch (msg)
            {
                case OCG_GameMessage.ConfirmDeckTop:
                    return new ConfirmDeckTopMessage(player, cards);
                case OCG_GameMessage.ConfirmCards:
                    return new ConfirmCardsMessage(player, cards);
                case OCG_GameMessage.ConfirmExtraTop:
                    return new ConfirmExtraDeckTopMessage(player, cards);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}