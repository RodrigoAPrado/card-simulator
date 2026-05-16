using System.Collections.Generic;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message
{
    public interface IAnnounceRaceMessage : ISelectionDuelMessage
    {
        byte Player { get; }
        byte Count { get; }
        IReadOnlyList<MonsterType> AvailableTypes { get; }
    }
}