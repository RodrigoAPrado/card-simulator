using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class SpecialSummonedMessage : SimpleTextMessage, ISpecialSummonedMessage
    {
        public SpecialSummonedMessage(string message = "Monster Special Summoned!") : base(message)
        {
        }
    }
}