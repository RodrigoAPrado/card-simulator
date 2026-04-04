using System;
using Ygo.Core.Phases.Abstract;

namespace Ygo.Core.Phases
{
    public class BattlePhase : BaseGamePhase
    {
        public override string Name => "Battle Phase";
        public BattlePhase(IGamePhase nextPhase, CardsHandler cardsHandler, Action advancePhase) 
            : base(nextPhase, cardsHandler, advancePhase)
        {
        }
    }
}