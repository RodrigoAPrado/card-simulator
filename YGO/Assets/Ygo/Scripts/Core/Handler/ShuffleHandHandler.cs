using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Ygo.Core.Duel;
using Ygo.Scripts.Core.Event.Base;
using Ygo.Scripts.Core.Handler.Base;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;

namespace Ygo.Scripts.Core.Handler
{
    public class ShuffleHandHandler : BaseHandler<IShuffleHandMessage>   
    {
        public override UniTask<IReadOnlyList<IEvent>> HandleMessage(IShuffleHandMessage message, DuelState duelState)
        {
            return UniTask.FromResult<IReadOnlyList<IEvent>>(duelState.ShuffleHand(message.Player, message.Cards));
        }
    }
}