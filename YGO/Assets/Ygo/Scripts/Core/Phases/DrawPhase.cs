using System;
using Ygo.Core.Phases.Abstract;

namespace Ygo.Core.Phases
{
    public class DrawPhase : BaseGamePhase
    {
        public override string Name => "Draw Phase";
        public DrawPhase(IGamePhase nextPhase, CardsHandler cardsHandler, Action advancePhase) 
            : base(nextPhase, cardsHandler, advancePhase)
        {
        }
        
        public override bool DrawFromDeck()
        {
            _cardsHandler.DrawCards(1);
            AdvancePhase();
            return true;
        }
    }
}