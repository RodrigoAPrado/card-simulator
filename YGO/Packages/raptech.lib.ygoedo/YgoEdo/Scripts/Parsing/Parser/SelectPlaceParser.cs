using System;
using System.Collections.Generic;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Constant;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Parser.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Util;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Parser
{
    public class SelectPlaceParser : BaseParser
    {
        protected override IOcgMessage DoParse(byte[] buffer)
        {
            var reader = new PacketReader(buffer);
            var msg = (OCG_GameMessage)reader.ReadByte();
            var player = reader.ReadByte();
            var amount = reader.ReadByte();
            var mask = reader.ReadUInt32();

            var zones = new List<OCG_Zone>();

            for (var i = 0; i < 32; i++)
                if ((mask & (1u << i)) == 0)
                    zones.Add((OCG_Zone)(1u << i));

            switch (msg)
            {
                case OCG_GameMessage.SelectPlace:
                    return new SelectPlaceMessage(player, amount, zones);
                case OCG_GameMessage.SelectDisfield:
                    return new SelectDisfieldMessage(player, amount, zones);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}