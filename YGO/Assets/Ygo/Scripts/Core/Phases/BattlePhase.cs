using System;
using Ygo.Core.Phases.Abstract;

namespace Ygo.Core.Phases
{
    public class BattlePhase : BaseGamePhase
    {
        public override string Name => "Battle Phase";
        public BattlePhase(IGamePhase nextPhase, Action advancePhase) 
            : base(nextPhase, advancePhase)
        {
        }
    }
}