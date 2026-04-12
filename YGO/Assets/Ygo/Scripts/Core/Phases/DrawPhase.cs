using System;
using System.Collections.Generic;
using System.Linq;
using Ygo.Core.Actions;
using Ygo.Core.Actions.Abstract;
using Ygo.Core.Enums;
using Ygo.Core.Phases.Abstract;
using Ygo.Core.Response;
using Ygo.Core.Response.Context;
using Ygo.Core.Response.Enum;

namespace Ygo.Core.Phases
{
    public class DrawPhase : BaseGamePhase
    {
        
        public DrawPhase(TurnContext context, GameState gameState) : base(context, gameState)
        {
        }

        public override GamePhase Phase => GamePhase.DrawPhase;

        public override void Init()
        {
            ChangeStep(PhaseStep.WaitingDraw);
        }

        public override ActionQuery ClickedOnMainDeck(Guid requesterId, Guid ownerId)
        {
            if (CurrentStep != PhaseStep.WaitingDraw)
                return new ActionQuery(requesterId, ownerId, ActionState.IncorrectStep);
            if (ownerId != Context.CurrentTurnPlayer.Id)
                return new ActionQuery(requesterId, ownerId, ActionState.IncorrectPlayer);
                
            var drawAction = new DrawAction(GameState, ownerId);
            return new ActionQuery(
                requesterId,
                ownerId, 
                new List<IGameAction>() { drawAction }, 
                new MainDeckInteractionContext(ownerId)
                );
        }
        
        public override ActionResult DrawForTurn(Guid ownerId)
        {
            if (CurrentStep != PhaseStep.WaitingDraw)
                return new ActionResult(ownerId, ActionState.IncorrectStep);
            if (ownerId != Context.CurrentTurnPlayer.Id)
                return new ActionResult(ownerId, ActionState.IncorrectPlayer);
            
            var result = Context.Players.FirstOrDefault(x => x.Id == ownerId)!.CardsHandler.TryDrawFromDeck();

            if (!result)
                return new ActionResult(ownerId, ActionState.CannotDrawFromDeck);
            ChangeStep(PhaseStep.ProceedToNextPhase);
            return new ActionResult(ownerId, ActionState.Success);
        }
    }
}