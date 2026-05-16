using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class FlipSummonedMessage : SimpleTextMessage, IFlipSummonedMessage
    {
        public FlipSummonedMessage(string message = "Monster Flip Summoned!") : base(message)
        {
        }
    }
}