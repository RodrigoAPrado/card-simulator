using System;
using Ygo.Core.Enums;
using Ygo.Core.Phases.Abstract;

namespace Ygo.Core.Phases
{
    public class StandbyPhase : BaseGamePhase
    {
        public StandbyPhase(TurnContext context, GameState gameState) : base(context, gameState)
        {
        }

        public override GamePhase Phase => GamePhase.StandbyPhase;

        public override void Init()
        {
            ChangeStep(GameStep.ProceedToNextPhase);
        }
    }
}