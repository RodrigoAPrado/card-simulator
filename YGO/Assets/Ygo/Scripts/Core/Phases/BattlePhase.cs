using System;
using System.Collections.Generic;
using System.Linq;
using Ygo.Core.Abstract;
using Ygo.Core.Enums;
using Ygo.Core.Phases.Abstract;
using Ygo.Core.Response;

namespace Ygo.Core.Phases
{
    public class BattlePhase : BaseGamePhase
    {
        public BattlePhase(TurnContext context, GameState gameState) : base(context, gameState)
        {
        }

        public override GamePhase Phase => GamePhase.BasePhase;

        public override void Init()
        {
            if (Context.CurrentTurn <= 1)
            {
                ChangeStep(GameStep.ProceedToNextPhase);
                return;
            }
            ChangeStep(GameStep.Battle);
        }
    }
}