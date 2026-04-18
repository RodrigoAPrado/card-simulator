using System;
using System.Collections.Generic;
using System.Linq;
using Ygo.Core.Abstract;
using Ygo.Core.Actions.Abstract;
using Ygo.Core.Board.Abstract;
using Ygo.Core.Enums;
using Ygo.Core.Response;
using Ygo.Core.Response.Enum;

namespace Ygo.Core.Phases.Abstract
{
    public abstract class BaseGamePhase : IGamePhase
    {
        public string Name => Phase.ToString();
        public abstract GamePhase Phase { get; }
        public IGamePhase NextPhase { get; }
        public bool HasNextPhase => NextPhase != null;
        public PhaseStep CurrentStep => _currentStep;
        protected TurnContext Context { get; }
        protected GameState GameState { get; }
        private PhaseStep _currentStep;
        protected BaseGamePhase(TurnContext context, GameState gameState)
        {
            Context = context;
            GameState = gameState;
        }

        public abstract void Init();
        public virtual ActionQuery ClickedOnMainDeck(Guid requesterId, Guid ownerId) 
            => new(requesterId, ownerId,ActionState.NotImplemented);

        public virtual ActionQuery ClickedOnCardInHand(Guid requesterId, Guid ownerId, ICardInstance card)
            => new(requesterId, ownerId,ActionState.NotImplemented);

        public virtual ActionQuery ClickedOnCardOnField(Guid requesterId, Guid ownerId, ICardInstance card)
            => new(requesterId, ownerId,ActionState.NotImplemented);

        public virtual ActionQuery ClickedOnZone(Guid requesterId, Guid ownerId, IBoardZone zone)
            => new(requesterId, ownerId,ActionState.NotImplemented);

        public virtual ActionQuery ClickedOnNextPhase(Guid requesterId)
            => new(requesterId, requesterId, ActionState.NotImplemented);

        protected void ChangeStep(PhaseStep step)
        {
            _currentStep = step;
        }

        public virtual ActionResult DrawForTurn(Guid ownerId) 
            => new(ownerId, ActionState.NotImplemented);
        public virtual ActionResult CheckNormalSummon(Guid ownerId, ICardInstance card) 
            => new(ownerId, ActionState.NotImplemented);
        public virtual ActionResult CheckNormalSet(Guid ownerId, ICardInstance card) 
            => new(ownerId, ActionState.NotImplemented);
        public virtual ActionResult DoNormalSummon(Guid ownerId, ICardInstance card, IBoardZone boardZone)
            => new(ownerId, ActionState.NotImplemented);
        public virtual ActionResult DoNormalSet(Guid ownerId, ICardInstance card, IBoardZone boardZone)
            => new(ownerId, ActionState.NotImplemented);
        public virtual ActionResult DoFlipSummon(Guid ownerId, ICardInstance card)
            => new(ownerId, ActionState.NotImplemented);
        public virtual ActionResult DoTryFlip(Guid ownerId, ICardInstance card)
            => new(ownerId, ActionState.NotImplemented);
        public virtual ActionResult DoSwitchMonsterToAttack(Guid ownerId, ICardInstance card)
            => new(ownerId, ActionState.NotImplemented);
        public virtual ActionResult DoSwitchMonsterToDefense(Guid ownerId, ICardInstance card)
            => new(ownerId, ActionState.NotImplemented);
        public virtual ActionResult CheckAttack(Guid ownerId, ICardInstance attacker)
            => new(ownerId, ActionState.NotImplemented);
        public virtual ActionResult DeclareAttack(Guid ownerId, ICardInstance attacker, ICardInstance defender)
            => new(ownerId, ActionState.NotImplemented);
        public virtual ActionResult DeclareDirectAttack(Guid ownerId, ICardInstance attacker)
            => new(ownerId, ActionState.NotImplemented);

        public virtual ActionResult FinishBattleState()
            => new(Guid.Empty, ActionState.NotImplemented);
    }
}