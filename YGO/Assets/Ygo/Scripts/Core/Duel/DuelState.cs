using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Ygo.Scripts.Core.Card;
using Ygo.Scripts.Core.Command;
using Ygo.Scripts.Core.Command.Base;
using Ygo.Scripts.Core.Duelist;
using Ygo.Scripts.Core.Enum;
using Ygo.Scripts.Core.Handler.Base;
using Ygo.Scripts.Core.Model;
using Ygo.Scripts.Data;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;

namespace Ygo.Core.Duel
{
    public class DuelState
    {
        public DuelInteraction CurrentInteraction { get; private set; }
        public DuelistState Player0State { get; }
        public DuelistState Player1State { get; }
        public byte CurrentTurn { get; private set; }
        public DuelPhase CurrentPhase { get; private set; }
        public byte CurrentTurnPlayer { get; private set; }
        private readonly DuelData _duelData;
        
        public DuelState(DuelData data)
        {
            CurrentInteraction = DuelInteraction.Proceed;
            Player0State = new DuelistState(data.Duelist0, 0);
            Player1State = new DuelistState(data.Duelist1, 1);
            CurrentTurn = 0;
            CurrentTurnPlayer = 0;
            CurrentPhase = DuelPhase.BeforeTheDrawPhase;
            _duelData = data;
        }

        public ICommand DrawCard(uint cardCode, byte player)
        {
            DuelistState playerState = player == 0 ? Player0State : Player1State;
            List<CardModel> handBefore = new List<CardModel>();
            foreach (var cardInHand in playerState.Hand)
            {
                var cardModel = new CardModel()
                {
                    Data = cardInHand.Data,
                    Sequence = cardInHand.Sequence,
                    Position = cardInHand.Position,
                    CardLocation = cardInHand.CardLocation,
                    Controller = cardInHand.Controller,
                };
                handBefore.Add(cardModel);
            }
            
            CardState card = playerState.TakeCard(cardCode, Location.Hand);
            var drawnCard = new CardModel()
            {
                Data = card.Data,
                Sequence = card.Sequence,
                Position = card.Position,
                CardLocation = card.CardLocation,
                Controller = card.Controller,
            };
            
            playerState.PutCard(card, Location.Hand);
            
            List<CardModel> handAfter = new List<CardModel>();
            foreach (var cardInHand in playerState.Hand)
            {
                var cardModel = new CardModel()
                {
                    Data = cardInHand.Data,
                    Sequence = cardInHand.Sequence,
                    Position = cardInHand.Position,
                    CardLocation = cardInHand.CardLocation,
                    Controller = cardInHand.Controller,
                };
                handAfter.Add(cardModel);
            }

            return new DrawCommand(handBefore, handAfter, drawnCard);
        }
    }
}