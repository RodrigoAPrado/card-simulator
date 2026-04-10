using System;
using Ygo.Core.Enums;
using Ygo.Core.Phases.Abstract;

namespace Ygo.Core.Phases
{
    public class MainPhase2 : MainPhase1
    {
        public MainPhase2(TurnContext context, GameState gameState) : base(context, gameState)
        {
        }
        
        public override GamePhase Phase => GamePhase.MainPhase2;

        public override void Init()
        {
            if (Context.CurrentTurn <= 1)
            {
                ChangeStep(GameStep.ProceedToNextPhase);
                return;
            }
            base.Init();
        }
    }
}