using System.Collections.Generic;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message
{
    public interface ISelectUnselectCardMessage : ISelectionDuelMessage
    {
        byte Player { get; }
        bool Finish { get; }
        uint Min { get; }
        uint Max { get; }
        IReadOnlyList<ICardReference> CardsToSelect { get; }
        IReadOnlyList<ICardReference> CardsToUnselect { get; }
    }
}