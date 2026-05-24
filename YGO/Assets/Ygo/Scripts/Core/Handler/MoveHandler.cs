using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Ygo.Core.Duel;
using Ygo.Scripts.Core.Event.Base;
using Ygo.Scripts.Core.Handler.Base;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;

namespace Ygo.Scripts.Core.Handler
{
    public class MoveHandler : BaseHandler<IMoveMessage>
    {
        public override UniTask<IReadOnlyList<IEvent>> HandleMessage(IMoveMessage message, DuelState duelState)
        {
            Debug.Log("[MoveMessage]");
            foreach (var r in message.Reasons)
            {
                Debug.Log($"Reason: {r}");
            }

            return UniTask.FromResult<IReadOnlyList<IEvent>>(duelState.MoveCard(
                message.CardCode, 
                message.OldLocation,
                message.NewLocation));
        }
    }
}