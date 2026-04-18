using System;
using Ygo.Core.Board.Abstract;
using Ygo.Core.Enums;
using Ygo.Data;
using Ygo.Data.Enums;

namespace Ygo.Core.Abstract
{
    public interface ICardInstance
    {
        Guid Id { get; }
        Guid OwnerId { get; }
        Guid ControllerId { get; }
        CardData Data { get; }
        CardLocation Location { get; }
        IBoardZone Zone { get; }
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
        bool CanNormalSummon { get; }
        bool CanNormalSet { get; }
        bool CanAttack { get; }
        bool IsSummoned { get; }
        bool IsFaceDown { get; }
        bool CanChangePosition { get; }
        bool IsInDefense { get; }
        bool IsDestroyed { get; }
        bool IsDestroyedByBattle { get; }
        bool IsDestroyedByCardEffect { get; }
        int? TributeCost { get; }
        void Summon(IBoardZone zone);
        void Set(IBoardZone zone);
        void AddToHand();
        void AddToMainDeck();
        void SetAttacked();
        void Flip();
        void FlipSummon();
        void PassTurn();
        void ChangePosition(bool defense);
        void DestroyByBattle();
        void DestroyByCardEffect();
        void SendToGraveyard();
        void ClearDestroyed();
    }
}