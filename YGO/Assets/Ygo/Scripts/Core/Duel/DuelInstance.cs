using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Ygo.Scripts.Core.Event.Base;
using Ygo.Scripts.Core.Handler.Base;
using Ygo.Scripts.Data;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;

namespace Ygo.Core.Duel
{
    public class DuelInstance
    {
        public IReadOnlyDictionary<uint, ICardData> CardsInDuel { get; } 
        public EventQueue EventQueue { get; }
        private readonly DuelBridge _bridge;
        private readonly DuelState _state;
        private readonly DuelData _duelData;
        private readonly HandlerRegistry _handlerRegistry;

        public DuelInstance(DuelData duelData, DuelBridge bridge, DuelState state, EventQueue eventQueue)
        {
            _duelData = duelData;
            var cardsInDuelList = new List<ICardData>();
            cardsInDuelList.AddRange(duelData.Duelist0.MainDeck);
            cardsInDuelList.AddRange(duelData.Duelist0.ExtraDeck);
            cardsInDuelList.AddRange(duelData.Duelist1.MainDeck);
            cardsInDuelList.AddRange(duelData.Duelist1.ExtraDeck);
            
            var cardsInDuelDictionary = new Dictionary<uint, ICardData>();

            foreach (var card in cardsInDuelList.Where(card => !cardsInDuelDictionary.TryAdd(card.Code, card)))
            {
                continue;
            }
            
            CardsInDuel = cardsInDuelDictionary;
            
            _bridge = bridge;
            _state = state;
            EventQueue = eventQueue;
            
            _handlerRegistry = HandlerRegistry.Create();
        }
        
        public async UniTask RunDuel()
        {
            var duelProceed = _bridge.ProceedDuel();;
            while (duelProceed)
            {
                bool nextMessage;

                do
                {
                    var duelMessage = _bridge.GetMessage();
                    var events = await HandleMessage(duelMessage);
                    foreach (var e in events)
                    {
                        EventQueue.EnqueueEvent(e);
                    }
                    nextMessage = _bridge.NextMessage();
                } while (nextMessage);

                duelProceed = _bridge.ProceedDuel();
            }
        }

        public async UniTask SetResponse(List<int> response)
        {
            if (!_bridge.SetResponse(response))
            {
                Debug.LogError("Failed to set response.");
                var message = _state.MessageAwaitingInput;
                _state.ClearMessageAwaitingInput();
                var events = await HandleMessage(message);
                foreach (var e in events)
                {
                    EventQueue.EnqueueEvent(e);
                }
                return;
            }
            _state.ClearMessageAwaitingInput();
            _ = RunDuel();
        }
        
        private async UniTask<IReadOnlyList<IEvent>> HandleMessage(IDuelMessage duelMessage)
        {
            if (duelMessage == null)
                throw new InvalidOperationException("Duel message is null.");
            IReadOnlyList<IEvent> events;
            var handler = _handlerRegistry.GetHandler(duelMessage);
            
            if (handler == null)
            {
                events = new List<IEvent>();
                Debug.Log($"{duelMessage.GetType()}\n{duelMessage}");
            }
            else
            {
                events = await handler.HandleMessage(duelMessage, _state);
            }
            
            return events;
        }
    }
}