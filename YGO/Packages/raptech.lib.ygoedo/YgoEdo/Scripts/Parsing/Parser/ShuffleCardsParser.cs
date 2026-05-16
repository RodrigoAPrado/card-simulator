using System.Collections.Generic;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Constant;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Parser.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Util;
using ArgumentOutOfRangeException = System.ArgumentOutOfRangeException;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Parser
{
    public class ShuffleCardsParser : BaseParser
    {
        protected override IOcgMessage DoParse(byte[] buffer)
        {
            var reader = new PacketReader(buffer);
            var msgType = (OCG_GameMessage)reader.ReadByte();
            var player = reader.ReadByte();
            var count = reader.ReadUInt32();
            var cards = new List<uint>();
            for (var i = count; i > 0; i--) cards.Add(reader.ReadUInt32());

            switch (msgType)
            {
                case OCG_GameMessage.ShuffleHand:
                    return new ShuffleHandMessage(player, cards);
                case OCG_GameMessage.ShuffleExtra:
                    return new ShuffleExtraMessage(player, cards);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}