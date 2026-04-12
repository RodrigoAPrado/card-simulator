using System;
using System.Collections.Generic;
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
        public GameStep CurrentStep => _currentStep;
        protected TurnContext Context { get; }
        protected GameState GameState { get; }
        private GameStep _currentStep;
        protected BaseGamePhase(TurnContext context, GameState gameState)
        {
            Context = context;
            GameState = gameState;
        }

        public abstract void Init();
        public virtual ActionQuery ClickedOnMainDeck(Guid requesterId, Guid ownerId) 
            => new(ownerId,ActionState.NotImplemented);

        public virtual ActionQuery ClickedOnCardInHand(Guid requesterId, Guid ownerId, ICardInstance card)
            => new(ownerId,ActionState.NotImplemented);

        public virtual ActionQuery ClickedOnCardOnField(Guid requesterId, Guid ownerId, ICardInstance card)
            => new(ownerId,ActionState.NotImplemented);

        public virtual ActionQuery ClickedOnZone(Guid requesterId, Guid ownerId, IBoardZone zone)
            => new(ownerId,ActionState.NotImplemented);

        public virtual ActionQuery ClickedOnNextPhase(Guid requesterId)
            => new(requesterId,ActionState.NotImplemented);

        protected void ChangeStep(GameStep step)
        {
            _currentStep = step;
        }

        public virtual ActionResult DrawCard(Guid playerId) 
            => new(playerId, ActionState.NotImplemented);
        public virtual ActionResult CheckNormalSummon(Guid playerId, ICardInstance card) 
            => new(playerId, ActionState.NotImplemented);
        public virtual ActionResult CheckNormalSet(Guid playerId, ICardInstance card) 
            => new(playerId, ActionState.NotImplemented);
        public virtual ActionResult DoNormalSummon(Guid playerId, ICardInstance card, IBoardZone boardZone)
            => new(playerId, ActionState.NotImplemented);
        public virtual ActionResult DoNormalSet(Guid playerId, ICardInstance card, IBoardZone boardZone)
            => new(playerId, ActionState.NotImplemented);
        public virtual ActionResult DoFlipSummon(Guid playerId, ICardInstance card)
            => new(playerId, ActionState.NotImplemented);
        public virtual ActionResult DoSwitchMonsterToAttack(Guid playerId, ICardInstance card)
            => new(playerId, ActionState.NotImplemented);
        public virtual ActionResult DoSwitchMonsterToDefense(Guid playerId, ICardInstance card)
            => new(playerId, ActionState.NotImplemented);
        public virtual ActionResult CheckAttack(Guid playerId, ICardInstance card)
            => new(playerId, ActionState.NotImplemented);
        public ActionResult DeclareAttack(Guid playerId, ICardInstance attacker, ICardInstance defender)
            => new(playerId, ActionState.NotImplemented);
        public ActionResult DeclareDirectAttack(Guid playerId, ICardInstance attacker)
            => new(playerId, ActionState.NotImplemented);
    }
}