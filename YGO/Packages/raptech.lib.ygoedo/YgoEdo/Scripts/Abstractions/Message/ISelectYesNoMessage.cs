using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message
{
    public interface ISelectYesNoMessage : ISelectionDuelMessage
    {
        byte Player { get; }
        string Description { get; }
    }
}