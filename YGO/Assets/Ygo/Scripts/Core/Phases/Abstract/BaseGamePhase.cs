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
        public virtual ActionQuery ClickedOnMainDeck(Guid playerId) 
            => new(playerId,ActionState.NotImplemented);

        protected void ChangeStep(GameStep step)
        {
            _currentStep = step;
        }

        public virtual ActionResult DrawCard(Guid playerId) => new(playerId, ActionState.NotImplemented);
    }
}