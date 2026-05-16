namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr
{
    public abstract class SimpleTextMessage : BaseMessage
    {
        protected SimpleTextMessage(string message)
        {
            Message = message;
        }

        private string Message { get; }

        public override string ToString()
        {
            return Message;
        }
    }
}