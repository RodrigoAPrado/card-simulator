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
            ChangeStep(GameStep.WaitingDraw);
        }

        public override ActionQuery ClickedOnMainDeck(Guid playerId)
        {
            if (CurrentStep != GameStep.WaitingDraw)
                return new ActionQuery(playerId, ActionState.IncorrectStep);
            if (playerId != Context.CurrentTurnPlayer.Id)
                return new ActionQuery(playerId, ActionState.IncorrectPlayer);
                
            var drawAction = new DrawAction(GameState, playerId);
            return new ActionQuery(
                playerId, 
                new List<IGameAction>() { drawAction }, 
                new DeckInteractionContext(playerId)
                );
        }
        
        public override ActionResult DrawCard(Guid playerId)
        {
            if (CurrentStep != GameStep.WaitingDraw)
                return new ActionResult(playerId, ActionState.IncorrectStep);
            if (playerId != Context.CurrentTurnPlayer.Id)
                return new ActionResult(playerId, ActionState.IncorrectPlayer);
            
            var result = Context.Players.FirstOrDefault(x => x.Id == playerId)!.CardsHandler.TryDrawFromDeck();

            if (!result)
                return new ActionResult(playerId, ActionState.CannotDrawFromDeck);
            ChangeStep(GameStep.ProceedToNextPhase);
            return new ActionResult(playerId, ActionState.Success);
        }
    }
}