using System;
using Ygo.Core.Phases.Abstract;

namespace Ygo.Core.Phases
{
    public class MainPhase1 : BaseGamePhase
    {
        public override string Name => "Main Phase 1";
        public MainPhase1(IGamePhase nextPhase, CardsHandler cardsHandler, Action advancePhase) 
            : base(nextPhase, cardsHandler, advancePhase)
        {
        }
    }
}