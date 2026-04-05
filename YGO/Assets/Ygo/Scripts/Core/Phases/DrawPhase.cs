using System;
using Ygo.Core.Phases.Abstract;

namespace Ygo.Core.Phases
{
    public class DrawPhase : BaseGamePhase
    {
        public override string Name => "Draw Phase";
        public DrawPhase(IGamePhase nextPhase, Action advancePhase) 
            : base(nextPhase, advancePhase)
        {
        }
        
        public override bool DrawFromDeck()
        {
            var result = _context.CurrentTurnPlayer.DrawFromDeck();
            if (!result)
                throw new NotImplementedException();
            AdvancePhase();
            return true;
        }
    }
}