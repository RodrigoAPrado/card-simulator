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
        public virtual GameStep CurrentStep => GameStep.None;

        private Action _advancePhase;
        protected TurnContext _context;
        protected BaseGamePhase(IGamePhase nextPhase, Action advancePhase)
        {
            NextPhase = nextPhase;
            _advancePhase = advancePhase;
        }

        public virtual void Init(TurnContext context)
        {
            _context = context;
        }

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

        protected void AdvancePhase()
        {
            _advancePhase?.Invoke();
        }
    }
}