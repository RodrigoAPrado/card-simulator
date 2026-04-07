using System;
using Ygo.Core.Abstract;
using Ygo.Core.Board.Abstract;
using Ygo.Core.Enums;
using Ygo.Core.Response;

namespace Ygo.Core.Phases.Abstract
{
    public abstract class BaseGamePhase : IGamePhase
    {
        public abstract string Name { get; }
        public IGamePhase NextPhase { get; }
        public bool HasNextPhase => NextPhase != null;
        public GameStep CurrentStep => _currentStep;
        
        protected TurnContext _context;
        private GameStep _currentStep;
        private Action _onGameStepChanged;
        
        protected BaseGamePhase(TurnContext context, Action onGameStepChanged)
        {
            _context = context;
            _onGameStepChanged = onGameStepChanged;
        }

        public abstract void Init();

        public virtual bool DrawFromDeck()
        {
            return false;
        }

        public virtual ClickedOnCardHandResponse ClickedOnCardInHand(ICardInstance card)
            => new ClickedOnCardHandResponse(null);

        public virtual WhereToSummonMonsterResponse CheckWhereToSummonMonster(ICardInstance card) => null;
        public virtual void CancelSummoning() { }

        public virtual bool SummonCardOnSelectedZone(ICardInstance card, IBoardZone zone) => false;
        public virtual void ToOpenGameStep() { }
        public virtual void GoToNextPhase() { }

        protected void ChangeStep(GameStep step)
        {
            _currentStep = step;
            _onGameStepChanged?.Invoke();
        }
    }
}