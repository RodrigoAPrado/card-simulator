using System;
using Ygo.Core.Abstract;
using Ygo.Core.Response;

namespace Ygo.Core.Phases.Abstract
{
    public abstract class BaseGamePhase : IGamePhase
    {
        public abstract string Name { get; }
        public IGamePhase NextPhase { get; }
        public bool HasNextPhase => NextPhase != null;

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
            => new ClickedOnCardHandResponse(true);

        protected void AdvancePhase()
        {
            _advancePhase?.Invoke();
        }
    }
}