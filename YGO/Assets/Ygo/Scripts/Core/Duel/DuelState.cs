using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Ygo.Scripts.Core.Card;
using Ygo.Scripts.Core.Duelist;
using Ygo.Scripts.Core.Enum;
using Ygo.Scripts.Core.Event;
using Ygo.Scripts.Core.Event.Base;
using Ygo.Scripts.Core.Field;
using Ygo.Scripts.Core.Handler.Base;
using Ygo.Scripts.Core.Model;
using Ygo.Scripts.Data;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component;

namespace Ygo.Core.Duel
{
    public class DuelState
    {
        public DuelInteraction CurrentInteraction =>
            MessageAwaitingInput == null ? DuelInteraction.Proceed : DuelInteraction.WaitingInput;
        public DuelistState Player0State { get; }
        public DuelistState Player1State { get; }
        public byte CurrentTurn { get; private set; }
        public DuelPhase CurrentPhase { get; private set; }
        public byte CurrentTurnPlayer { get; private set; }
        public IDuelMessage MessageAwaitingInput { get; private set; }
        private readonly DuelData _duelData;
        private readonly FieldState _fieldState;
        
        public DuelState(DuelData data)
        {
            Player0State = new DuelistState(data.Duelist0, 0);
            Player1State = new DuelistState(data.Duelist1, 1);
            CurrentTurn = 0;
            CurrentTurnPlayer = 1;
            CurrentPhase = DuelPhase.BeforeTheDrawPhase;
            _duelData = data;
            _fieldState = new FieldState();
        }

        public PointOfView GetPointOfView(byte player)
        {
            return player == _duelData.PlayerId ? PointOfView.Player : PointOfView.Opponent;
        }

        public ICardData GetCardData(uint cardCode, byte player, Location location)
        {
            if ((location & Location.OnField) == Location.OnField)
            {
                return _fieldState.TryGetCardStateFromCardCode(cardCode)?.Data;
            }
            var playerState = player == 0 ? Player0State : Player1State;
            var cardState = playerState.GetCardByCode(cardCode, location);
            
            if(cardState == null)
                throw new ArgumentOutOfRangeException(nameof(cardCode), cardCode, null);
            
            return cardState.Data;
        }

        public IEvent ChangeTurn()
        {
            CurrentTurnPlayer++;
            if (CurrentTurnPlayer > 1)
                CurrentTurnPlayer = 0;
            CurrentTurn++;
            return new NewTurnEvent(CurrentTurn, GetPointOfView(CurrentTurnPlayer));
        }

        public IEvent ChangePhase(DuelPhase phase)
        {
            CurrentPhase = phase;
            return new NewPhaseEvent(phase);
        }

        public List<IEvent> DrawCard(uint cardCode, byte player)
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

            var deckAmountBefore = playerState.MainDeck.Count;
            
            CardState card = playerState.TakeCard(cardCode, Location.Deck, -1);
            var drawnCard = new CardModel()
            {
                Data = card.Data,
                Sequence = card.Sequence,
                Position = card.Position,
                CardLocation = card.CardLocation,
                Controller = card.Controller,
            };
            
            playerState.PutCard(card, Location.Hand);
            card.UpdateState(CardPosition.FaceDown, Location.Hand, player);
            
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

            var pov = GetPointOfView(player);

            return new List<IEvent>()
            {
                new DrawDeckEvent(
                    deckAmountBefore,
                    playerState.MainDeck.Count,
                    pov
                    ),
                new DrawHandEvent(
                    handBefore,
                    handAfter,
                    drawnCard,
                    pov
                )
            };
        }

        public void SetMessageAwaitingInput(IDuelMessage message)
        {
            if(MessageAwaitingInput != null)
                throw new InvalidOperationException("Cannot change the message while the current state is set.");
            MessageAwaitingInput = message;
        }

        public void ClearMessageAwaitingInput()
        {
            MessageAwaitingInput = null;
        }

        public List<IEvent> MoveCard(uint cardCode, IFullLocationReference before, IFullLocationReference after)
        {
            var playerState = before.Controller == 0 ? Player0State : Player1State;
            var cardState = before.Location == Location.OnField
                ? _fieldState.TakeCard(cardCode, before, _duelData.PlayerId)
                : playerState.TakeCard(cardCode, before.Location, (int) before.Sequence);
            
            if (after.Location == Location.OnField)
            {
                _fieldState.PutCard(cardState, after, _duelData.PlayerId);
            }
            else
            {
                playerState.PutCard(cardState, after.Location);
            }
            
            cardState.UpdateState(after);
            var fieldStateAfter = _fieldState.GetFieldZone(after, _duelData.PlayerId);
            
            return new List<IEvent>()
            {
                new MoveEvent(cardCode, 
                    before.Location, 
                    (int)before.Sequence,
                    _fieldState.GetFieldZone(before, _duelData.PlayerId), 
                    before.Controller, 
                    before.Position, 
                    GetPointOfView(before.Controller),
                    after.Location, 
                    (int)after.Sequence, 
                    fieldStateAfter,
                    after.Controller, 
                    after.Position, 
                    GetPointOfView(after.Controller), 
                    new CardModel()
                    {
                        Data = cardState.Data, 
                        Sequence = cardState.Sequence, 
                        Position = cardState.Position,
                        CardLocation = cardState.CardLocation,
                        CardFieldZone = fieldStateAfter,
                        Controller = after.Controller,
                        Description = cardState.Data.Description
                    })
            };
        }
    }
}