using System.Collections.Generic;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message
{
    public interface ISelectCounterMessage : ISelectionDuelMessage
    {
        byte Player { get; }
        CounterType CounterType { get; }
        ushort CounterAmount { get; }
        IReadOnlyList<ICardReference> Cards { get; }
    }
}