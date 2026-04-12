using System;
using System.Collections.Generic;
using System.Linq;
using Ygo.Core.Abstract;
using Ygo.Core.Actions;
using Ygo.Core.Actions.Abstract;
using Ygo.Core.Enums;
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
                ChangeStep(GameStep.ProceedToNextPhase);
                return;
            }
            ChangeStep(GameStep.Battle);
        }

        public override ActionQuery ClickedOnCardOnField(Guid requesterId, Guid ownerId, ICardInstance card)
        {
            if (requesterId != Context.CurrentTurnPlayer.Id || ownerId != Context.CurrentTurnPlayer.Id)
                return new ActionQuery(requesterId, ActionState.IncorrectPlayer);
            
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


        private ActionQuery OnClickedOnMonsterOnFieldOnBattle(Guid requesterId, Guid playerId,
            ICardInstance card)
        {
            var actionList = new List<IGameAction>();
            if(card.CanAttack)
                actionList.Add(new MonsterAttackAction(GameState, playerId, card));
            actionList.Add(new CancelAction(GameState));
            return new ActionQuery(playerId, actionList, new CardInteractionContext(playerId, card));
        }
        
        public override ActionQuery ClickedOnNextPhase(Guid requesterId)
        {
            if(requesterId != Context.CurrentTurnPlayer.Id)
                return new ActionQuery(requesterId, ActionState.IncorrectPlayer);
            if(CurrentStep != GameStep.Battle)
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
    }
}