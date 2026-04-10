using System;
using Ygo.Core.Events.Abstract;

namespace Ygo.Core.Events
{
    public class PlayerShouldDrawEvent : IGameEvent
    {
        public Guid PlayerId { get; }

        public PlayerShouldDrawEvent(Guid playerId)
        {
            PlayerId = playerId;
        }
    }
}