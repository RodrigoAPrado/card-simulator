using System;
using Ygo.Core.Phases.Abstract;

namespace Ygo.Core.Phases
{
    public class StandbyPhase : BaseGamePhase
    {
        public override string Name => "Standby Phase";
        public StandbyPhase(IGamePhase nextPhase, Action advancePhase) 
            : base(nextPhase, advancePhase)
        {
        }

        public override void Init(TurnContext context)
        {
            AdvancePhase();
        }
    }
}