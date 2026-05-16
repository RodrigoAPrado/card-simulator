using System.Collections.Generic;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class TossDiceMessage : BaseMessage, ITossDiceMessage
    {
        public TossDiceMessage(byte player, List<byte> results)
        {
            Player = player;
            Results = results;
        }

        public byte Player { get; }
        public IReadOnlyList<byte> Results { get; }

        public override string ToString()
        {
            return $"TossCoin, Player={Player}, Results=[{string.Join(", ", Results)}]";
        }
    }
}