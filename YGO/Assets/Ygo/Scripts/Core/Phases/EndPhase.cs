using System;
using Ygo.Core.Enums;
using Ygo.Core.Phases.Abstract;

namespace Ygo.Core.Phases
{
    public class EndPhase : BaseGamePhase
    {
        public EndPhase(TurnContext context) : base(context)
        {
        }

        public override GamePhase Phase => GamePhase.EndPhase;

        public override void Init()
        {
            ChangeStep(GameStep.ProceedToNextPhase);
        }
    }
}