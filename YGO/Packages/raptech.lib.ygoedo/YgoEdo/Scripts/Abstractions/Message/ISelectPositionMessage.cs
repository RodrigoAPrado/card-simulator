using System.Collections.Generic;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message
{
    public interface ISelectPositionMessage : ISelectionDuelMessage
    {
        byte Player { get; }
        uint CardCode { get; }
        IReadOnlyList<CardPosition> PositionAvailable { get; }
    }
}