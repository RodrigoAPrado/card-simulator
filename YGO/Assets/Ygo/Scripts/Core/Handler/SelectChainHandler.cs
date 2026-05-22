using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Ygo.Core.Duel;
using Ygo.Scripts.Core.Event;
using Ygo.Scripts.Core.Event.Base;
using Ygo.Scripts.Core.Handler.Base;
using Ygo.Scripts.Core.Model;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;

namespace Ygo.Scripts.Core.Handler
{
    public class SelectChainHandler : BaseHandler<ISelectChainMessage>
    {
        public override UniTask<IReadOnlyList<IEvent>> HandleMessage(ISelectChainMessage message, DuelState duelState)
        {
            var cards = new List<CardModel>();
            
            foreach (var effect in message.Effects)
            {
                var cardData = duelState.GetCardData(effect.Code, message.Player, effect.LocationReference.Location);
                cards.Add(new CardModel()
                {
                    Data = cardData,
                    Sequence = effect.LocationReference.Sequence,
                    Position = effect.LocationReference.Position,
                    CardLocation = effect.LocationReference.Location,
                    Controller = effect.LocationReference.Controller,
                    Description = effect.Description
                });
            }
            
            duelState.SetMessageAwaitingInput(message);

            return new UniTask<IReadOnlyList<IEvent>>(new List<IEvent>
                { new SelectChainEvent(cards, !message.Forced, duelState.GetPointOfView(message.Player), message.Player) });
        }
    }
}