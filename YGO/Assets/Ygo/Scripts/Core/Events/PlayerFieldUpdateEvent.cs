using System;
using System.Collections.Generic;
using Ygo.Core.Board.Abstract;
using Ygo.Core.Events.Abstract;

namespace Ygo.Core.Events
{
    public class PlayerFieldUpdateEvent : IGameEvent
    {
        public Guid PlayerId { get; }

        public PlayerFieldUpdateEvent(Guid playerId)
        {
            PlayerId = playerId;
        }
    }
}