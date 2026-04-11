using System;
using System.Collections.Generic;
using Ygo.Core.Abstract;
using Ygo.Core.Actions;
using Ygo.Core.Actions.Abstract;
using Ygo.Core.Board.Abstract;
using Ygo.Core.Enums;
using Ygo.Core.Phases.Abstract;
using Ygo.Core.Response;
using Ygo.Core.Response.Context;
using Ygo.Core.Response.Enum;
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

        public override ActionQuery ClickedOnCardInHand(Guid playerId, ICardInstance card)
        {
            if (CurrentStep == GameStep.Open)
            {
                
            }
            return new ActionQuery(playerId, ActionState.NotImplemented);
        }

        private ActionQuery OnClickedOnCardInHandOnGameStateOpen(Guid playerId, ICardInstance card)
        {
            if(Context.CurrentTurnPlayer.Id != playerId)
                return new ActionQuery(playerId, ActionState.IncorrectPlayer);
            if (card.Location != CardLocation.Hand)
                throw new InvalidOperationException("Card location is not hand");
            
            switch (card.Data.CardType)
            {
                case CardType.Monster:
                    return OnClickedOnMonsterInHandOnGameStateOpen(playerId, card);
                case CardType.Spell:
                    throw new NotImplementedException();
                case CardType.Trap:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private ActionQuery OnClickedOnMonsterInHandOnGameStateOpen(Guid playerId, ICardInstance card)
        {
            var actionList = new List<IGameAction>();
            if(card.CanNormalSummon)
                actionList.Add(new NormalSummonAction(GameState, playerId, card));
            
            if(card.CanNormalSet)
                actionList.Add(new NormalSetAction(GameState, playerId, card));
            
            actionList.Add(new CancelAction(GameState));

            return new ActionQuery(playerId, actionList, new CardInteractionContext(playerId, card));
        }
    }
}