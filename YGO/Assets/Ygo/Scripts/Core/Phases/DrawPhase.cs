using System;
using Ygo.Core.Enums;
using Ygo.Core.Phases.Abstract;

namespace Ygo.Core.Phases
{
    public class DrawPhase : BaseGamePhase
    {
        public override string Name => "Draw Phase";
        
        public DrawPhase(TurnContext context, Action onGameStepChanged) : base(context, onGameStepChanged)
        {
        }

        public override void Init()
        {
            ChangeStep(GameStep.WaitingDraw);
        }

        public override bool DrawFromDeck()
        {
            if (CurrentStep != GameStep.WaitingDraw)
            {
                return false;
            }
            
            var result = _context.CurrentTurnPlayer.DrawFromDeck();
            if (!result)
                throw new NotImplementedException();
            ChangeStep(GameStep.ProceedToNextPhase);
            return true;
        }
    }
}