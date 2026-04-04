using System;
using Ygo.Core.Abstract;
using Ygo.Core.Phases.Abstract;

namespace Ygo.Core.Phases
{
    public class MainPhase1 : BaseGamePhase
    {
        public override string Name => "Main Phase 1";

        private bool _normalSummoned;
        
        public MainPhase1(IGamePhase nextPhase, CardsHandler cardsHandler, Action advancePhase) 
            : base(nextPhase, cardsHandler, advancePhase)
        {
        }

        public override void Init()
        {
            _normalSummoned = false;
        }

        public override void ClickedOnCardInHand(ICardInstance card)
        {
            if (_normalSummoned)
            {
                return;
            }
            
        }

        protected override void Clear()
        {
            
        }
    }
}