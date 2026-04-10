using System;
using Ygo.Core.Enums;
using Ygo.Core.Phases.Abstract;

namespace Ygo.Core.Phases
{
    public class MainPhase2 : MainPhase1
    {
        public MainPhase2(TurnContext context, Action onGameStepChanged) : base(context, onGameStepChanged)
        {
        }
        
        public override GamePhase Phase => GamePhase.MainPhase2;

        public override void Init()
        {
            if (_context.CurrentTurn <= 1)
            {
                ChangeStep(GameStep.ProceedToNextPhase);
                return;
            }
            base.Init();
        }
    }
}