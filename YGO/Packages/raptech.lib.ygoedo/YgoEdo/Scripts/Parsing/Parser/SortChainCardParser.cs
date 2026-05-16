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
    public class SortChainCardParser : BaseParser
    {
        protected override IOcgMessage DoParse(byte[] buffer)
        {
            var reader = new PacketReader(buffer);
            var msg = (OCG_GameMessage)reader.ReadByte(); //msg
            var player = reader.ReadByte();
            var count = reader.ReadUInt32();

            var cards = new List<CardReference>();

            for (var i = count; i > 0; i--)
            {
                var cardCode = reader.ReadUInt32();
                var controller = reader.ReadByte();
                var location = (OCG_CardLocation)reader.ReadUInt32();
                var sequence = reader.ReadUInt32();
                cards.Add(new CardReference(cardCode, new FullLocationReference(controller, location, sequence, 0),
                    count - i));
            }

            return msg switch
            {
                OCG_GameMessage.SortCard => new SortCardMessage(player, cards),
                OCG_GameMessage.SortChain => new SortChainMessage(player, cards),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}