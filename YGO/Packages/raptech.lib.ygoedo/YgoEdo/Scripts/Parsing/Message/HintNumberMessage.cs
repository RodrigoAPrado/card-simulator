using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class HintNumberMessage : BaseMessage, IHintNumberMessage
    {
        public HintNumberMessage(byte player, ulong number)
        {
            Player = player;
            Number = number;
        }

        public byte Player { get; }
        public ulong Number { get; }

        public override string ToString()
        {
            return $"Hint: Player={Player}, Number={Number}";
        }
    }
}