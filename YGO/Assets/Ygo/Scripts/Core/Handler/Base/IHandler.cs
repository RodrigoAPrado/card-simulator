using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Ygo.Core.Duel;
using Ygo.Scripts.Core.Event.Base;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;

namespace Ygo.Scripts.Core.Handler.Base
{
    public interface IHandler
    {
        UniTask<IReadOnlyList<IEvent>> HandleMessage(IDuelMessage message, DuelState duelState);
    }
    
    public interface IHandler<in T> : IHandler where T : IDuelMessage
    {
        UniTask<IReadOnlyList<IEvent>> HandleMessage(T message, DuelState duelState);
    }
}