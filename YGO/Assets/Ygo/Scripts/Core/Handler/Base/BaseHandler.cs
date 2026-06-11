using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Ygo.Core.Duel;
using Ygo.Scripts.Core.Event.Base;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;

namespace Ygo.Scripts.Core.Handler.Base
{
    public abstract class BaseHandler<T> : IHandler<T> where T : IDuelMessage
    {
        public abstract UniTask<IReadOnlyList<IEvent>> HandleMessage(T message, DuelState duelState);

        public UniTask<IReadOnlyList<IEvent>> HandleMessage(IDuelMessage message, DuelState duelState)
        {
            Debug.Log($"Handling message: {message.GetType()}\n{message}");
            return HandleMessage((T) message, duelState);
        }
    }
}