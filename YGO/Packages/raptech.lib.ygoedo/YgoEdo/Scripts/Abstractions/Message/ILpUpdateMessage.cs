using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message
{
    public interface ILpUpdateMessage : IDuelMessage
    {
        byte Player { get; }
        uint Lp { get; }
    }
}