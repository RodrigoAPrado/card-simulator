using System;
using Ygo.Data;
using Ygo.Data.Enums;

namespace Ygo.Core.Abstract
{
    public interface ICardInstance
    {
        Guid Id { get; }
        CardData Data { get; }
        int? CurrentLevel { get; }
        int? CurrentAtk { get; }
        int? CurrentDef { get; }
        bool IsValidMonster { get; }
        bool IsPendulum { get; }
        bool IsRitual { get; }
        bool IsFusion { get; }
        bool IsSynchro { get; }
        bool IsXyz { get; }
        bool IsLink { get; }
        bool IsEffect { get; }
        string CardText { get; }
    }
}