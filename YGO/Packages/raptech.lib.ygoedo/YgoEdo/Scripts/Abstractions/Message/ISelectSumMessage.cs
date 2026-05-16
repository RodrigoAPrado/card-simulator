using System.Collections.Generic;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message
{
    public interface ISelectSumMessage : ISelectionDuelMessage
    {
        byte Player { get; }
        bool HasMaximumChoices { get; }
        uint TargetSum { get; }
        uint MinimumChoices { get; }
        uint MaximumChoices { get; }
        IReadOnlyList<ICardReference> MustSelect { get; }
        IReadOnlyList<ICardReference> CanSelect { get; }
    }
}