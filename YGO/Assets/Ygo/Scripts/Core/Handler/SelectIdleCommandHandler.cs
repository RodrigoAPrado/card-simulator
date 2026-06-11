using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Ygo.Core.Duel;
using Ygo.Scripts.Core.Enum;
using Ygo.Scripts.Core.Event;
using Ygo.Scripts.Core.Event.Base;
using Ygo.Scripts.Core.Handler.Base;
using Ygo.Scripts.Core.Model;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component.Command.Idle;

namespace Ygo.Scripts.Core.Handler
{
    public class SelectIdleCommandHandler : BaseHandler<ISelectIdleCommandMessage>
    {
        public override UniTask<IReadOnlyList<IEvent>> HandleMessage(ISelectIdleCommandMessage message, DuelState duelState)
        {
            var cardIdleCommandList = new List<CardIdleCommandModel>();
            var battlePhaseIndex = -1;
            var endPhaseIndex = -1;
            var shuffleHandIndex = -1;
            for (var i = 0; i < message.Choices.Count; i++)
            {
                var command = message.Choices[i];
                Action<CardIdleCommandModel, int> action = null;
                CardCommandModel cardModel = null;
                string effect = null;
                
                switch (command)
                {
                    case IIdleNormalSummon typedCommand:
                        action = CardIdleCommandModel.AddNormalSummon;
                        cardModel = new CardCommandModel()
                        {
                            CardCode = typedCommand.CardCode, 
                            PointOfView = duelState.GetPointOfView(typedCommand.Controller), 
                            Location = typedCommand.Location, 
                            Sequence = typedCommand.Sequence,
                        };
                        break;
                    case IIdleSpecialSummon typedCommand:
                        action = CardIdleCommandModel.AddSpecialSummon;
                        cardModel = new CardCommandModel()
                        {
                            CardCode = typedCommand.CardCode, 
                            PointOfView = duelState.GetPointOfView(typedCommand.Controller), 
                            Location = typedCommand.Location, 
                            Sequence = typedCommand.Sequence,
                        };
                        break;
                    case IIdleChangeCardPosition typedCommand:
                        action = CardIdleCommandModel.AddChangeCardPosition;
                        cardModel = new CardCommandModel()
                        {
                            CardCode = typedCommand.CardCode, 
                            PointOfView = duelState.GetPointOfView(typedCommand.Controller), 
                            Location = typedCommand.Location, 
                            Sequence = typedCommand.Sequence,
                        };
                        break;
                    case IIdleSet typedCommand:
                        action = CardIdleCommandModel.AddSet;
                        cardModel = new CardCommandModel()
                        {
                            CardCode = typedCommand.CardCode, 
                            PointOfView = duelState.GetPointOfView(typedCommand.Controller), 
                            Location = typedCommand.Location, 
                            Sequence = typedCommand.Sequence,
                        };
                        break;
                    case IIdleSpellOrTrapSet typedCommand:
                        action = CardIdleCommandModel.AddSpellOrTrapSet;
                        cardModel = new CardCommandModel()
                        {
                            CardCode = typedCommand.CardCode, 
                            PointOfView = duelState.GetPointOfView(typedCommand.Controller), 
                            Location = typedCommand.Location, 
                            Sequence = typedCommand.Sequence,
                        };
                        break;
                    case IIdleEffectActivation typedCommand:
                        cardModel = new CardCommandModel()
                        {
                            CardCode = typedCommand.CardCode, 
                            PointOfView = duelState.GetPointOfView(typedCommand.Controller), 
                            Location = typedCommand.Location, 
                            Sequence = typedCommand.Sequence,
                        };
                        effect = typedCommand.Description;
                        break;
                    case IIdleToBattlePhase typedCommand:
                        battlePhaseIndex = i;
                        break;
                    case IIdleToEndPhase typedCommand:
                        endPhaseIndex = i;
                        break;
                    case IIdleShuffleHand typedCommand:
                        shuffleHandIndex = i;
                        break;
                }

                if (string.IsNullOrEmpty(effect) && action == null)
                    continue;

                var card = cardIdleCommandList.FirstOrDefault(x
                    => x.Location == cardModel.Location
                       && x.Sequence == cardModel.Sequence
                       && x.CardCode == cardModel.CardCode
                       && x.PointOfView == cardModel.PointOfView);
                
                if (card == null)
                {
                    card = new CardIdleCommandModel(
                        cardModel.CardCode, 
                        cardModel.PointOfView, 
                        cardModel.Sequence, 
                        cardModel.Location
                        );
                    cardIdleCommandList.Add(card);
                }

                if (effect == null)
                {
                    action.Invoke(card, i);
                }
                else
                {
                    CardIdleCommandModel.AddEffectActivation(card, i, effect);
                }
            }

            return UniTask.FromResult<IReadOnlyList<IEvent>>(new IEvent[] {
                new SelectIdleCommandEvent(
                    message.Player, 
                    duelState.GetPointOfView(message.Player), 
                    battlePhaseIndex, 
                    endPhaseIndex, 
                    shuffleHandIndex, 
                    cardIdleCommandList
                    )
            });
        }
        
        private class CardCommandModel
        {
            internal uint CardCode { get; set; }
            internal PointOfView PointOfView { get; set; }
            internal uint Sequence { get; set; }
            internal Location Location { get; set; }
        }
    }
}