using System;
using Ygo.Core.Abstract;
using Ygo.Core.Board.Abstract;
using Ygo.Core.Enums;
using Ygo.Core.Phases.Abstract;
using Ygo.Core.Response;
using Ygo.Data.Enums;

namespace Ygo.Core.Phases
{
    public class MainPhase1 : BaseGamePhase
    {
        public MainPhase1(TurnContext context, GameState gameState) : base(context, gameState)
        {
        }

        public override GamePhase Phase => GamePhase.MainPhase1;

        public override void Init()
        {
            ChangeStep(GameStep.Open);
        }
    }
}