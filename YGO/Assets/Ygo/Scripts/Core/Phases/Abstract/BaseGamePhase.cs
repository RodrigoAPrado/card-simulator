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

        protected CardsHandler _cardsHandler;
        private Action _advancePhase;
        protected BaseGamePhase(IGamePhase nextPhase, CardsHandler cardsHandler, Action advancePhase)
        {
            NextPhase = nextPhase;
            _cardsHandler = cardsHandler;
            _advancePhase = advancePhase;
        }

        public virtual void Init() { }

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