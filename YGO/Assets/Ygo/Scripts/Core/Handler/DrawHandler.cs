using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Ygo.Core.Duel;
using Ygo.Scripts.Core.Event.Base;
using Ygo.Scripts.Core.Handler.Base;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;

namespace Ygo.Scripts.Core.Handler
{
    public class DrawHandler : BaseHandler<IDrawMessage>
    {
        public override UniTask<IReadOnlyList<IEvent>> HandleMessage(IDrawMessage message, DuelState duelState)
        {
            List<IEvent> events = new List<IEvent>();
            
            foreach (var card in message.Cards)
            {
                events.AddRange(duelState.DrawCard(card.CardCode, message.Player));
            }

            return new UniTask<IReadOnlyList<IEvent>>(events);
        }
    }
}