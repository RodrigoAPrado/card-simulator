using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message
{
    public interface IPayLpCostMessage : IDuelMessage
    {
        byte Player { get; }
        uint Cost { get; }
    }
}