using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class ChainSolvingMessage : BaseMessage, IChainSolvingMessage
    {
        public ChainSolvingMessage(byte chainLink)
        {
            ChainLink = chainLink;
        }

        public byte ChainLink { get; }

        public override string ToString()
        {
            return $"SolvingChain, ChainLink={ChainLink}";
        }
    }
}