using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Ygo.Core.Duel;
using Ygo.Scripts.Core.Event.Base;
using Ygo.Scripts.Core.Handler.Base;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;

namespace Ygo.Scripts.Core.Handler
{
    public class NewPhaseHandler : BaseHandler<INewPhaseMessage>
    {
        public override UniTask<IReadOnlyList<IEvent>> HandleMessage(INewPhaseMessage message, DuelState duelState)
        {
            return new UniTask<IReadOnlyList<IEvent>>(new List<IEvent>{duelState.ChangePhase(message.GamePhase)});
        }
    }
}