using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.System.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class HandResMessage : BaseMessage, IHandResMessage
    {
        public RockPaperScissors Player0 { get; }
        public RockPaperScissors Player1 { get; }

        public HandResMessage(RockPaperScissors player0, RockPaperScissors player1)
        {
            Player0 = player0;
            Player1 = player1;
        }

        public override string ToString()
        {
            return $"Hand Res Message: Player0={Player0}, Player1={Player1}";
        }
    }
}