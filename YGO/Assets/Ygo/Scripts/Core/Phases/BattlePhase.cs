using System;
using System.Collections.Generic;
using System.Linq;
using Ygo.Core.Abstract;
using Ygo.Core.Actions;
using Ygo.Core.Actions.Abstract;
using Ygo.Core.Enums;
using Ygo.Core.Interaction;
using Ygo.Core.Phases.Abstract;
using Ygo.Core.Response;
using Ygo.Core.Response.Context;
using Ygo.Core.Response.Enum;
using Ygo.Data.Enums;

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
                ChangeStep(PhaseStep.ProceedToNextPhase);
                return;
            }
            ChangeStep(PhaseStep.Open);
        }

        public override ActionQuery ClickedOnCardOnField(Guid requesterId, Guid ownerId, ICardInstance card)
        {
            if (CurrentStep != PhaseStep.Open)
                return new ActionQuery(requesterId, ownerId, ActionState.IncorrectStep);
            
            if (requesterId != Context.CurrentTurnPlayer.Id || ownerId != Context.CurrentTurnPlayer.Id)
                return new ActionQuery(requesterId, ownerId, ActionState.IncorrectPlayer);
            
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
                    return OnClickedOnMonsterOnFieldOnBattle(requesterId, ownerId, card);
                case CardType.Spell:
                    throw new NotImplementedException();
                case CardType.Trap:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private ActionQuery OnClickedOnMonsterOnFieldOnBattle(Guid requesterId, Guid ownerId,
            ICardInstance card)
        {
            var actionList = new List<IGameAction>();
            if(card.CanAttack)
                actionList.Add(new MonsterAttackAction(GameState, ownerId, card));
            actionList.Add(new CancelAction(GameState));
            return new ActionQuery(requesterId, ownerId, actionList, new CardInteractionContext(ownerId, card));
        }

        public override ActionResult CheckAttack(Guid ownerId, ICardInstance attacker)
        {
            if (ownerId != Context.CurrentTurnPlayer.Id)
                throw new InvalidOperationException("Player has not been on the current turn");
            
            if (attacker.Location 
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

            if (!attacker.CanAttack)
                throw new InvalidOperationException("Card cannot attack");

            List<ICardInstance> availableCards = Context.OpponentPlayer.BoardHandler.MonsterZones.Where(x => !x.IsFree)
                .Select(x => x.CardInZone).ToList();

            if (availableCards.Count <= 0)
            {
                GameState.DeclareDirectAttack(ownerId, attacker);
            }
            else
            {
                GameState.SetInteractionState(Context.OpponentPlayer.Id, 
                    new AttackTargetSelectState(ownerId, 
                        GameState, 
                        availableCards, 
                        attacker));
            }
            
            return new ActionResult(ownerId, ActionState.Success);
        }


        public override ActionResult DeclareAttack(Guid ownerId, ICardInstance attacker, ICardInstance defender)
        {
            GameState.SetBattleState(new BattleState(GameState, ownerId, Context.OpponentPlayer.Id, attacker, defender));
            ChangeStep(PhaseStep.Battle);
            return new ActionResult(ownerId, ActionState.Success);
        }

        public override ActionResult DeclareDirectAttack(Guid ownerId, ICardInstance attacker)
        {
            GameState.SetBattleState(new DirectBattleState(GameState, ownerId, Context.OpponentPlayer.Id, attacker));
            ChangeStep(PhaseStep.Battle);
            return new ActionResult(ownerId, ActionState.Success);
        }

        public override ActionResult DoTryFlip(Guid ownerId, ICardInstance card)
        {
            if (!card.IsFaceDown) 
                throw new InvalidOperationException("Card is already face-up");
            
            card.Flip();
            return new ActionResult(ownerId, ActionState.Success);
        }

        public override ActionQuery ClickedOnNextPhase(Guid requesterId)
        {
            if(requesterId != Context.CurrentTurnPlayer.Id)
                return new ActionQuery(requesterId,Guid.Empty, ActionState.IncorrectPlayer);
            if(CurrentStep != PhaseStep.Open)
                return new ActionQuery(requesterId,Guid.Empty, ActionState.IncorrectStep);
            ChangeStep(PhaseStep.ProceedToNextPhase);
            return new ActionQuery(
                requesterId,Guid.Empty,
                new List<IGameAction>()
                {
                    new EmptyAction()
                }, 
                new NoContext(requesterId)
            );
        }

        public override ActionResult FinishBattleState()
        {
            if (CurrentStep != PhaseStep.Battle)
                return new ActionResult(Guid.Empty, ActionState.IncorrectStep);
            GameState.ClearBattleState();
            ChangeStep(PhaseStep.Open);
            return new ActionResult(Guid.Empty, ActionState.Success);
        }
        
        
    }
}