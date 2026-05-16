using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class ChainEndMessage : SimpleTextMessage, IChainEndMessage
    {
        public ChainEndMessage(string message) : base(message)
        {
        }
    }
}