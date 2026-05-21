using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Ygo.Core.Duel;
using Ygo.Scripts.Core.Event.Base;
using Ygo.Scripts.Core.Handler.Base;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;

namespace Ygo.Scripts.Core.Handler
{
    public class DrawHandler : IHandler<IDrawMessage>
    {
        public UniTask<IReadOnlyList<IEvent>> HandleMessage(IDrawMessage message, DuelState duelState)
        {
            List<IEvent> commands = new List<IEvent>();
            
            foreach (var card in message.Cards)
            {
                commands.Add(duelState.DrawCard(card.CardCode, message.Player));
            }

            return new UniTask<IReadOnlyList<IEvent>>(commands);
        }

        public async UniTask<IReadOnlyList<IEvent>> HandleMessage(IDuelMessage message, DuelState duelState)
        {
            return await HandleMessage((IDrawMessage)message, duelState);
        }
    }
}