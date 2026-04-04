using System;
using Ygo.Core.Phases.Abstract;

namespace Ygo.Core.Phases
{
    public class StandbyPhase : BaseGamePhase
    {
        public override string Name => "Standby Phase";
        public StandbyPhase(IGamePhase nextPhase, CardsHandler cardsHandler, Action advancePhase) 
            : base(nextPhase, cardsHandler, advancePhase)
        {
        }

        public override void Init()
        {
            AdvancePhase();
        }
    }
}