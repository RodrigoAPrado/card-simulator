using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.System.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class RetryMessage : SimpleTextMessage, IRetryMessage
    {
        public RetryMessage(string message) : base(message)
        {
        }

        public override InputType Input => InputType.Retry;
    }
}