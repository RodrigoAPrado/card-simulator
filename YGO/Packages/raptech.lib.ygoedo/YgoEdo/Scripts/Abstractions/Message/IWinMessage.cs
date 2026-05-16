using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.System.Enum;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message
{
    public interface IWinMessage : IDuelMessage
    {
        byte Player { get; }
        SystemVictoryReason Reason { get; }
    }
}