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
            ChangeStep(PhaseStep.Open);
        }

        public override ActionQuery ClickedOnCardInHand(Guid requesterId, Guid ownerId, ICardInstance card)
        {
            if (CurrentStep == PhaseStep.Open)
            {
                return OnClickedOnCardInHandOnGameStateOpen(requesterId, ownerId, card);
            }
            return new ActionQuery(requesterId, ownerId, ActionState.IncorrectStep);
        }

        private ActionQuery OnClickedOnCardInHandOnGameStateOpen(Guid requesterId, Guid ownerId, ICardInstance card)
        {
            if(Context.CurrentTurnPlayer.Id != requesterId)
                return new ActionQuery(requesterId, ownerId, ActionState.IncorrectPlayer);
            if(Context.CurrentTurnPlayer.Id != ownerId)
                return new ActionQuery(requesterId, ownerId, ActionState.NotOwner);
            if (card.Location != CardLocation.Hand)
                throw new InvalidOperationException("Card location is not hand");
            
            switch (card.Data.CardType)
            {
                case CardType.Monster:
                    return OnClickedOnMonsterInHandOnGameStateOpen(requesterId, ownerId, card);
                case CardType.Spell:
                    throw new NotImplementedException();
                case CardType.Trap:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private ActionQuery OnClickedOnMonsterInHandOnGameStateOpen(Guid requesterId, Guid ownerId, ICardInstance card)
        {
            var canPlayerNormalSummon = !Context.CurrentTurnPlayer.NormalSummonFlag;
            var actionList = new List<IGameAction>();

            if (card.TributeCost <= 0)
            {
                var zoneFrees = Context.CurrentTurnPlayer.BoardHandler.MonsterZones.Any(x => x.IsFree);
                if(card.CanNormalSummon && zoneFrees && canPlayerNormalSummon)
                    actionList.Add(new NormalSummonAction(GameState, ownerId, card));
            
                if(card.CanNormalSet && zoneFrees && canPlayerNormalSummon)
                    actionList.Add(new NormalSetAction(GameState, ownerId, card));
            }
            else
            {
                var monsterAmount = Context.CurrentTurnPlayer.BoardHandler.MonsterZones.Count(x => !x.IsFree);
                if (monsterAmount >= card.TributeCost)
                {
                    if(card.CanNormalSummon && canPlayerNormalSummon)
                        actionList.Add(new TributeSummonAction(GameState, ownerId, card));
            
                    //if(card.CanNormalSet && canPlayerNormalSummon)
                      //  actionList.Add(new TributeS(GameState, ownerId, card));
                }   
            }
            
            
            actionList.Add(new CancelAction(GameState));

            return new ActionQuery(requesterId, ownerId, actionList, new CardInteractionContext(ownerId, card), true);
        }
        
        public override ActionQuery ClickedOnCardOnField(Guid requesterId, Guid ownerId, ICardInstance card)
        {
            if (CurrentStep == PhaseStep.Open)
            {
                return OnClickedOnCardOnFieldOnGameStateOpen(requesterId, ownerId, card);
            }
            return new ActionQuery(requesterId, ownerId, ActionState.NotImplemented);
        }
        
        private ActionQuery OnClickedOnCardOnFieldOnGameStateOpen(Guid requesterId, Guid ownerId, ICardInstance card)
        {
            if(Context.CurrentTurnPlayer.Id != requesterId)
                return new ActionQuery(requesterId, ownerId, ActionState.IncorrectPlayer);
            if(Context.CurrentTurnPlayer.Id != ownerId)
                return new ActionQuery(requesterId, ownerId, ActionState.NotOwner);
            if (card.Location 
                is not CardLocation.FieldZone
                and not CardLocation.LeftMostMonsterZone
                and not CardLocation.LeftCenterMonsterZone
                and not CardLocation.MiddleCenterMonsterZone
                and not CardLocation.RightCenterMonsterZone
                and not CardLocation.RightMostMonsterZone
                and not CardLocation.LeftMostSpellTrapZone
                and not CardLocation.LeftCenterSpellTrapZone
                and not CardLocation.MiddleCenterSpellTrapZone
                and not CardLocation.RightCenterSpellTrapZone
                and not CardLocation.RightMostSpellTrapZone)
                throw new InvalidOperationException("Card location is not on field");
            
            switch (card.Data.CardType)
            {
                case CardType.Monster:
                    return OnClickedOnMonsterOnFieldOnGameStateOpen(requesterId, ownerId, card);
                case CardType.Spell:
                    throw new NotImplementedException();
                case CardType.Trap:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private ActionQuery OnClickedOnMonsterOnFieldOnGameStateOpen(Guid requesterId, Guid ownerId, ICardInstance card)
        {
            var actionList = new List<IGameAction>();

            if (card.CanChangePosition)
            {
                if (card.IsInDefense)
                {
                    if (card.IsFaceDown)
                    {
                        actionList.Add(new FlipSummonAction(GameState, ownerId, card));
                    }
                    else
                    {
                        actionList.Add(new SwitchMonsterToAttackAction(GameState, ownerId, card));
                    }
                }
                else
                {
                    actionList.Add(new SwitchMonsterToDefenseAction(GameState, ownerId, card));
                }
            }
            
            actionList.Add(new CancelAction(GameState));

            return new ActionQuery(requesterId, ownerId, actionList, new CardInteractionContext(ownerId, card));
        }
        
        public override ActionQuery ClickedOnNextPhase(Guid requesterId)
        {
            if(requesterId != Context.CurrentTurnPlayer.Id)
                return new ActionQuery(requesterId, Guid.Empty, ActionState.IncorrectPlayer);
            if(CurrentStep != PhaseStep.Open)
                return new ActionQuery(requesterId, Guid.Empty, ActionState.IncorrectStep);
            ChangeStep(PhaseStep.ProceedToNextPhase);
            return new ActionQuery(
                requesterId,
                Guid.Empty, 
                new List<IGameAction>()
                {
                    new EmptyAction()
                }, 
                new NoContext(requesterId)
                );
        }
        
        public override ActionResult CheckNormalSummon(Guid playerId, ICardInstance card, bool isTribute)
        {
            if(!card.CanNormalSummon)
                throw new InvalidOperationException("Card cannot be normal summoned!");
            ValidateSummonContext(playerId, card);
            var availableZones = Context.CurrentTurnPlayer.BoardHandler.MonsterZones.Where(x => x.IsFree).ToList();
            if (!availableZones.Any())
                throw new InvalidOperationException("No zones available for summon!");

            GameState.SetInteractionState(playerId, 
                new NormalSummonZoneSelectState(playerId, 
                    GameState, 
                    availableZones, 
                    card,
                    isTribute));
            
            return new ActionResult(playerId, ActionState.Success);
        }

        public override ActionResult CheckNormalSet(Guid playerId, ICardInstance card, bool isTribute)
        {
            if(!card.CanNormalSet)
                throw new InvalidOperationException("Card cannot be normal set!");
            ValidateSummonContext(playerId, card);
            var availableZones = Context.CurrentTurnPlayer.BoardHandler.MonsterZones.Where(x => x.IsFree).ToList();
            if (!availableZones.Any())
                throw new InvalidOperationException("No zones available for summon!");
            
            GameState.SetInteractionState(playerId, 
                new NormalSetZoneSelectState(playerId, 
                    GameState, 
                    availableZones, 
                    card,
                    isTribute));
            
            return new ActionResult(playerId, ActionState.Success);
        }

        public override ActionResult RequestTributeSummonOrSet(Guid playerId, ICardInstance card, bool isSet)
        {
            if(!card.CanNormalSummon)
                throw new InvalidOperationException("Card cannot be normal summon!");
            ValidateSummonContext(playerId, card);
            GameState.SetInteractionState(playerId, new TributeSummonConfirmState(playerId, GameState, card, isSet));

            return new ActionResult(playerId, ActionState.Success);
        }
        
        public override ActionResult CheckAvailableTributesForSummonOrSet(Guid ownerId, ICardInstance card, bool isSet)
        {
            if(!card.CanNormalSet)
                throw new InvalidOperationException("Card cannot be normal set!");
            ValidateSummonContext(ownerId, card);

            var player = Context.GetPlayerById(ownerId);
            
            var cards = player.BoardHandler.MonsterZones
                .Where(x => !x.IsFree)
                .Select(x => x.CardInZone)
                .ToList();
            
            if(cards.Count < card.TributeCost)
                throw new InvalidOperationException($"Player does not have enough cards to tribute summon! " +
                                                    $"Has {cards.Count} but needs {card.TributeCost}");
            
            GameState.SetInteractionState(ownerId, new TributeSelectingState(ownerId, GameState, cards, card, isSet));   
            return new ActionResult(ownerId, ActionState.Success); 
        }

        private void ValidateSummonContext(Guid playerId, ICardInstance card)
        {
            if(Context.CurrentTurnPlayer.Id != playerId)
                throw new InvalidOperationException("Player has not been on the current turn");
            if (card.Location != CardLocation.Hand)
                throw new InvalidOperationException("Card location is not hand");
            if (Context.CurrentTurnPlayer.NormalSummonFlag)
                throw new InvalidOperationException("Player has already normal summoned!");
        }

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
        
        public override ActionResult DoNormalSet(Guid playerId, ICardInstance card, IBoardZone boardZone)
        {
            if(Context.CurrentTurnPlayer.Id != playerId)
                throw new InvalidOperationException("Player has not been on the current turn");
            if (card.Location != CardLocation.Hand)
                throw new InvalidOperationException("Card location is not hand");
            if(!card.CanNormalSet)
                throw new InvalidOperationException("Card cannot be normal summoned!");
            if (Context.CurrentTurnPlayer.NormalSummonFlag)
                throw new InvalidOperationException("Player has already normal summoned!");
            if(!boardZone.IsFree)
                throw new InvalidOperationException("Board zone (" + boardZone.Id + ") is not free.");

            card.Set(boardZone);
            boardZone.TryPutCard(card);
            Context.CurrentTurnPlayer.CardsHandler.RemoveCardFromHand(card);
            Context.CurrentTurnPlayer.SetNormalSummoned();
            return new ActionResult(playerId, ActionState.Success);
        }

        public override ActionResult DoFlipSummon(Guid playerId, ICardInstance card)
        {
            if(Context.CurrentTurnPlayer.Id != playerId)
                throw new InvalidOperationException("Player has not been on the current turn");
            if (card.Location 
                is not CardLocation.FieldZone
                and not CardLocation.LeftMostMonsterZone
                and not CardLocation.LeftCenterMonsterZone
                and not CardLocation.MiddleCenterMonsterZone
                and not CardLocation.RightCenterMonsterZone
                and not CardLocation.RightMostMonsterZone
                and not CardLocation.LeftMostSpellTrapZone
                and not CardLocation.LeftCenterSpellTrapZone
                and not CardLocation.MiddleCenterSpellTrapZone
                and not CardLocation.RightCenterSpellTrapZone
                and not CardLocation.RightMostSpellTrapZone)
                throw new InvalidOperationException("Card location is not on field");
            if (!card.IsFaceDown || !card.IsInDefense)
                throw new InvalidOperationException("Card is not face-down defense");
            card.FlipSummon();
            
            return new ActionResult(playerId, ActionState.Success);    
        }

        public override ActionResult DoSwitchMonsterToAttack(Guid playerId, ICardInstance card)
        {
            if(Context.CurrentTurnPlayer.Id != playerId)
                throw new InvalidOperationException("Player has not been on the current turn");
            if (card.Location 
                is not CardLocation.FieldZone
                and not CardLocation.LeftMostMonsterZone
                and not CardLocation.LeftCenterMonsterZone
                and not CardLocation.MiddleCenterMonsterZone
                and not CardLocation.RightCenterMonsterZone
                and not CardLocation.RightMostMonsterZone
                and not CardLocation.LeftMostSpellTrapZone
                and not CardLocation.LeftCenterSpellTrapZone
                and not CardLocation.MiddleCenterSpellTrapZone
                and not CardLocation.RightCenterSpellTrapZone
                and not CardLocation.RightMostSpellTrapZone)
                throw new InvalidOperationException("Card location is not on field");
            if (!card.IsInDefense)
                throw new InvalidOperationException("Card is not in defense");
            
            card.ChangePosition(false);
            return new ActionResult(playerId, ActionState.Success);    
        }

        public override ActionResult DoSwitchMonsterToDefense(Guid playerId, ICardInstance card)
        {
            if(Context.CurrentTurnPlayer.Id != playerId)
                throw new InvalidOperationException("Player has not been on the current turn");
            if (card.Location 
                is not CardLocation.FieldZone
                and not CardLocation.LeftMostMonsterZone
                and not CardLocation.LeftCenterMonsterZone
                and not CardLocation.MiddleCenterMonsterZone
                and not CardLocation.RightCenterMonsterZone
                and not CardLocation.RightMostMonsterZone
                and not CardLocation.LeftMostSpellTrapZone
                and not CardLocation.LeftCenterSpellTrapZone
                and not CardLocation.MiddleCenterSpellTrapZone
                and not CardLocation.RightCenterSpellTrapZone
                and not CardLocation.RightMostSpellTrapZone)
                throw new InvalidOperationException("Card location is not on field");
            if (card.IsInDefense)
                throw new InvalidOperationException("Card is not in attack");
            
            card.ChangePosition(true);
            return new ActionResult(playerId, ActionState.Success);    
        }
    }
}