using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class ChainSolvedMessage : BaseMessage, IChainSolvedMessage
    {
        public ChainSolvedMessage(byte chainCount)
        {
            ChainCount = chainCount;
        }

        public byte ChainCount { get; }

        public override string ToString()
        {
            return $"ChainSolved, ChainCount={ChainCount}";
        }
    }
}