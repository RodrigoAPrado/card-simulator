using System;
using System.Collections.Generic;
using Ygo.Core.Abstract;
using Ygo.Core.Board.Abstract;
using Ygo.Core.Enums;
using Ygo.Core.Response;

namespace Ygo.Core.Phases.Abstract
{
    public abstract class BaseGamePhase : IGamePhase
    {
        public string Name => Phase.ToString();
        public abstract GamePhase Phase { get; }
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

        public virtual ClickedOnCardResponse ClickedOnCardInHand(ICardInstance card)
            => new ClickedOnCardResponse(null);

        public virtual WhereToSummonMonsterResponse CheckWhereToSummonMonster(ICardInstance card) => null;
        public virtual void CancelSummoning() { }

        public virtual bool SummonCardOnSelectedZone(ICardInstance card, IBoardZone zone) => false;
        public virtual void ToOpenGameStep() { }
        public virtual void GoToNextPhase() { }

        public virtual ClickedOnCardResponse ClickedOnCardInField(ICardInstance card) 
            => new ClickedOnCardResponse(null);
        public virtual CheckAttackTargetsResponse CheckAttackTargets(ICardInstance card)
            => new CheckAttackTargetsResponse(null, new List<ICardInstance>());
        public virtual BattleResponse DeclareAttack(ICardInstance attacker, ICardInstance target) 
            => new BattleResponse(null, null);
        public virtual void ContinueTheDamageStep() { }
        
        protected void ChangeStep(GameStep step)
        {
            _currentStep = step;
        }
    }
}