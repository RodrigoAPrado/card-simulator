using System;
using Ygo.Core.Enums;
using Ygo.Core.Phases.Abstract;

namespace Ygo.Core.Phases
{
    public class StandbyPhase : BaseGamePhase
    {
        public override string Name => "Standby Phase";

        public StandbyPhase(TurnContext context, Action onGameStepChanged) : base(context, onGameStepChanged)
        {
        }

        public override void Init()
        {
            ChangeStep(GameStep.ProceedToNextPhase);
        }
    }
}