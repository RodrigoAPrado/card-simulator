using System;
using Ygo.Core.Enums;
using Ygo.Core.Phases.Abstract;

namespace Ygo.Core.Phases
{
    public class BattlePhase : BaseGamePhase
    {
        public override string Name => "Battle Phase";
        public BattlePhase(TurnContext context, Action onGameStepChanged) : base(context, onGameStepChanged)
        {
        }

        public override void Init()
        {
            if (_context.CurrentTurn <= 1)
            {
                ChangeStep(GameStep.ProceedToNextPhase);
                return;
            }

            throw new NotImplementedException();
        }
    }
}