using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Ygo.Scripts.Core.Card;
using Ygo.Scripts.Core.Duelist;
using Ygo.Scripts.Core.Enum;
using Ygo.Scripts.Core.Event;
using Ygo.Scripts.Core.Event.Base;
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
            CurrentTurnPlayer = 1;
            CurrentPhase = DuelPhase.BeforeTheDrawPhase;
            _duelData = data;
        }

        public IEvent ChangeTurn()
        {
            CurrentTurnPlayer++;
            if (CurrentTurnPlayer > 1)
                CurrentTurnPlayer = 0;
            CurrentTurn++;
            return new NewTurnEvent(CurrentTurn, _duelData.PlayerId == CurrentTurnPlayer 
                ? PointOfView.Player : PointOfView.Opponent);
        }

        public IEvent ChangePhase(DuelPhase phase)
        {
            CurrentPhase = phase;
            return new NewPhaseEvent(phase);
        }

        public IEvent DrawCard(uint cardCode, byte player)
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
            
            CardState card = playerState.TakeCard(cardCode, Location.Deck);
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

            return new DrawEvent(
                handBefore, 
                handAfter, 
                drawnCard, 
                _duelData.PlayerId == player ? PointOfView.Player : PointOfView.Opponent
                );
        }
    }
}