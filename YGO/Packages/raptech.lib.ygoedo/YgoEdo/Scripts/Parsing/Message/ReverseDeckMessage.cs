using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class ReverseDeckMessage : SimpleTextMessage, IReverseDeckMessage
    {
        public ReverseDeckMessage(string message = "Decks are reversed!") : base(message)
        {
        }
    }
}