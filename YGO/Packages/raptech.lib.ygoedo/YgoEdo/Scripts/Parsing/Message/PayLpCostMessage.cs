using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class PayLpCostMessage : BaseMessage, IPayLpCostMessage
    {
        public PayLpCostMessage(byte player, uint cost)
        {
            Player = player;
            Cost = cost;
        }

        public byte Player { get; }
        public uint Cost { get; }

        public override string ToString()
        {
            return $"Player {Player}, payed {Cost} LP!";
        }
    }
}