using System;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Constant;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Parser.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Util;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Parser
{
    public class ChangeCounterParser : BaseParser
    {
        protected override IOcgMessage DoParse(byte[] buffer)
        {
            var reader = new PacketReader(buffer);
            var msg = (OCG_GameMessage)reader.ReadByte(); //msg
            var counterType = (OCG_CounterType)reader.ReadUInt16();
            var player = reader.ReadByte();
            var location = (OCG_CardLocation)reader.ReadByte();
            var sequence = reader.ReadByte();
            var count = reader.ReadUInt16();

            switch (msg)
            {
                case OCG_GameMessage.AddCounter:
                    return new AddCounterMessage(counterType, player, location, sequence, count);
                case OCG_GameMessage.RemoveCounter:
                    return new RemoveCounterMessage(counterType, player, location, sequence, count);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}