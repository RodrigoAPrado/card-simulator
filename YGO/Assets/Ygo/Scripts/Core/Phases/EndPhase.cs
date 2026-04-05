using System;
using Ygo.Core.Phases.Abstract;

namespace Ygo.Core.Phases
{
    public class EndPhase : BaseGamePhase
    {
        public override string Name => "End Phase";
        public EndPhase(IGamePhase nextPhase, Action advancePhase) 
            : base(nextPhase, advancePhase)
        {
        }
    }
}