using System;
using Ygo.Core.Enums;
using Ygo.Data;
using Ygo.Data.Enums;

namespace Ygo.Core.Abstract
{
    public interface ICardInstance
    {
        Guid Id { get; }
        CardData Data { get; }
        CardLocation Location { get; }
        int? CurrentLevel { get; }
        int? CurrentAtk { get; }
        int? CurrentDef { get; }
        bool IsValidMonster { get; }
        bool IsValidSpell { get; }
        bool IsPendulum { get; }
        bool IsRitual { get; }
        bool IsFusion { get; }
        bool IsSynchro { get; }
        bool IsXyz { get; }
        bool IsLink { get; }
        bool IsEffect { get; }
        string CardText { get; }
        bool TreatedAsSpell { get; }
        bool TreatedAsTrap { get; }
        bool TreatedAsMonster { get; }
        bool IsField { get; }
        void SetLocation(CardLocation location);
    }
}