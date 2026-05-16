using System;
using System.Collections.Generic;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Constant;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Parser.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Util;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Parser
{
    public class TossCoinDiceParser : BaseParser
    {
        protected override IOcgMessage DoParse(byte[] buffer)
        {
            var reader = new PacketReader(buffer);
            var message = (OCG_GameMessage)reader.ReadByte(); //msg
            var player = reader.ReadByte();
            var count = reader.ReadByte();

            switch (message)
            {
                case OCG_GameMessage.TossCoin:
                    return GetTossCoin(reader, player, count);
                case OCG_GameMessage.TossDice:
                    return GetTossDice(reader, player, count);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private TossCoinMessage GetTossCoin(PacketReader reader, byte player, byte count)
        {
            var results = new List<OCG_CoinResult>();
            for (var i = count; i > 0; i--) results.Add((OCG_CoinResult)reader.ReadByte());

            return new TossCoinMessage(player, results);
        }

        private TossDiceMessage GetTossDice(PacketReader reader, byte player, byte count)
        {
            var results = new List<byte>();
            for (var i = count; i > 0; i--) results.Add(reader.ReadByte());

            return new TossDiceMessage(player, results);
        }
    }
}