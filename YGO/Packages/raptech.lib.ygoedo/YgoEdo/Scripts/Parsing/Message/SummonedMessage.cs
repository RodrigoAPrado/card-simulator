using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class SummonedMessage : SimpleTextMessage, ISummonedMessage
    {
        public SummonedMessage(string message = "Monster Summoned!") : base(message)
        {
        }
    }
}