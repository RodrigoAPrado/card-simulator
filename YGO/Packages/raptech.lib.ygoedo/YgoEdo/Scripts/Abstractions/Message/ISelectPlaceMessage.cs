using System.Collections.Generic;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message
{
    public interface ISelectPlaceMessage : ISelectionDuelMessage
    {
        byte Player { get; }
        uint Amount { get; }
        IReadOnlyList<FieldZones> Choices { get; }
    }
}