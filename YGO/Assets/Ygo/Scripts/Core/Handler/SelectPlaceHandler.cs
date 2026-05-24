using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Ygo.Core.Duel;
using Ygo.Scripts.Core.Event;
using Ygo.Scripts.Core.Event.Base;
using Ygo.Scripts.Core.Handler.Base;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;

namespace Ygo.Scripts.Core.Handler
{
    public class SelectPlaceHandler : BaseHandler<ISelectPlaceMessage>
    {
        public override UniTask<IReadOnlyList<IEvent>> HandleMessage(ISelectPlaceMessage message, DuelState duelState)
        {
            duelState.SetMessageAwaitingInput(message);
            return new UniTask<IReadOnlyList<IEvent>>(new
                List<IEvent>()
                {
                    new SelectPlaceEvent(message.Player, duelState.GetPointOfView(message.Player), message.CanCancel,
                        message.Amount, message.Choices)
                });
        }
    }
}