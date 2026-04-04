using System;
using Ygo.Core.Phases.Abstract;

namespace Ygo.Core.Phases
{
    public class MainPhase2 : BaseGamePhase
    {
        public override string Name => "Main Phase 2";
        public MainPhase2(IGamePhase nextPhase, CardsHandler cardsHandler, Action advancePhase) 
            : base(nextPhase, cardsHandler, advancePhase)
        {
        }
    }
}