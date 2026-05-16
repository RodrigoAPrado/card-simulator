using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message
{
    public interface ISelectEffectYesNoMessage : ISelectionDuelMessage
    {
        byte Player { get; }
        ICardReference Card { get; }
        string Description { get; }
    }
}