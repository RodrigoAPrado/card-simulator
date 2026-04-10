using System;
using System.Collections.Generic;
using Ygo.Core.Abstract;
using Ygo.Core.Events.Abstract;

namespace Ygo.Core.Events
{
    public class PlayerHandUpdateEvent : IGameEvent
    {
        public IList<ICardInstance> Hand { get; }
        public Guid PlayerId { get; }

        public PlayerHandUpdateEvent(IList<ICardInstance> hand, Guid playerId)
        {
            Hand = hand;
            PlayerId = playerId;
        }
    }
}