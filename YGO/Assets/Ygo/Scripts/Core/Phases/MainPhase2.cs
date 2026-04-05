using System;
using Ygo.Core.Phases.Abstract;

namespace Ygo.Core.Phases
{
    public class MainPhase2 : BaseGamePhase
    {
        public override string Name => "Main Phase 2";
        public MainPhase2(IGamePhase nextPhase, Action advancePhase) 
            : base(nextPhase, advancePhase)
        {
        }
    }
}