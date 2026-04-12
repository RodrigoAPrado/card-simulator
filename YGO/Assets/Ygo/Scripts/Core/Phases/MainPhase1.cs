using System;
using System.Collections.Generic;
using System.Linq;
using Ygo.Core.Abstract;
using Ygo.Core.Actions;
using Ygo.Core.Actions.Abstract;
using Ygo.Core.Board.Abstract;
using Ygo.Core.Enums;
using Ygo.Core.Interaction;
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

        public override ActionQuery ClickedOnCardInHand(Guid requesterId, Guid ownerId, ICardInstance card)
        {
            if (CurrentStep == GameStep.Open)
            {
                return OnClickedOnCardInHandOnGameStateOpen(requesterId, ownerId, card);
            }
            return new ActionQuery(ownerId, ActionState.NotImplemented);
        }

        private ActionQuery OnClickedOnCardInHandOnGameStateOpen(Guid requesterId, Guid playerId, ICardInstance card)
        {
            if(Context.CurrentTurnPlayer.Id != playerId)
                return new ActionQuery(playerId, ActionState.IncorrectPlayer);
            if (card.Location != CardLocation.Hand)
                throw new InvalidOperationException("Card location is not hand");
            
            switch (card.Data.CardType)
            {
                case CardType.Monster:
                    return OnClickedOnMonsterInHandOnGameStateOpen(requesterId, playerId, card);
                case CardType.Spell:
                    throw new NotImplementedException();
                case CardType.Trap:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private ActionQuery OnClickedOnMonsterInHandOnGameStateOpen(Guid requesterId, Guid playerId, ICardInstance card)
        {
            var actionList = new List<IGameAction>();

            var zoneFrees = Context.CurrentTurnPlayer.BoardHandler.MonsterZones.Any(x => x.IsFree);
            var canPlayerNormalSummon = !Context.CurrentTurnPlayer.NormalSummonFlag;
            
            if(card.CanNormalSummon && zoneFrees && canPlayerNormalSummon)
                actionList.Add(new NormalSummonAction(GameState, playerId, card));
            
            if(card.CanNormalSet && zoneFrees && canPlayerNormalSummon)
                actionList.Add(new NormalSetAction(GameState, playerId, card));
            
            actionList.Add(new CancelAction(GameState));

            return new ActionQuery(playerId, actionList, new CardInteractionContext(playerId, card));
        }

        public override ActionQuery ClickedOnNextPhase(Guid requesterId)
        {
            if(requesterId != Context.CurrentTurnPlayer.Id)
                return new ActionQuery(requesterId, ActionState.IncorrectPlayer);
            if(CurrentStep != GameStep.Open)
                return new ActionQuery(requesterId, ActionState.IncorrectStep);
            ChangeStep(GameStep.ProceedToNextPhase);
            return new ActionQuery(
                requesterId,
                new List<IGameAction>()
                {
                    new EmptyAction()
                }, 
                new NoContext(requesterId)
                );
        }
        
        public override ActionResult CheckNormalSummon(Guid playerId, ICardInstance card)
        {
            if(Context.CurrentTurnPlayer.Id != playerId)
                throw new InvalidOperationException("Player has not been on the current turn");
            if (card.Location != CardLocation.Hand)
                throw new InvalidOperationException("Card location is not hand");
            if(!card.CanNormalSummon)
                throw new InvalidOperationException("Card cannot be normal summoned!");
            if (Context.CurrentTurnPlayer.NormalSummonFlag)
                throw new InvalidOperationException("Player has already normal summoned!");
            var availableZones = Context.CurrentTurnPlayer.BoardHandler.MonsterZones.Where(x => x.IsFree).ToList();
            if (!availableZones.Any())
                throw new InvalidOperationException("No zones available for summon!");

            GameState.SetInteractionState(playerId, 
                new NormalSummonZoneSelectState(playerId, 
                    GameState, 
                    availableZones, 
                    card));
            
            return new ActionResult(playerId, ActionState.Success);
        }
        public override ActionResult CheckNormalSet(Guid playerId, ICardInstance card) 
            => new(playerId, ActionState.NotImplemented);

        public override ActionResult DoNormalSummon(Guid playerId, ICardInstance card, IBoardZone boardZone)
        {
            if(Context.CurrentTurnPlayer.Id != playerId)
                throw new InvalidOperationException("Player has not been on the current turn");
            if (card.Location != CardLocation.Hand)
                throw new InvalidOperationException("Card location is not hand");
            if(!card.CanNormalSummon)
                throw new InvalidOperationException("Card cannot be normal summoned!");
            if (Context.CurrentTurnPlayer.NormalSummonFlag)
                throw new InvalidOperationException("Player has already normal summoned!");
            if(!boardZone.IsFree)
                throw new InvalidOperationException("Board zone (" + boardZone.Id + ") is not free.");

            card.Summon(boardZone);
            boardZone.TryPutCard(card);
            Context.CurrentTurnPlayer.CardsHandler.RemoveCardFromHand(card);
            Context.CurrentTurnPlayer.SetNormalSummoned();
            return new ActionResult(playerId, ActionState.Success);
        }
    }
}