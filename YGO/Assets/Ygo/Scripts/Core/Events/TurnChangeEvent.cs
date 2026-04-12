using System;
using Ygo.Core.Events.Abstract;

namespace Ygo.Core.Events
{
    public class TurnChangeEvent : IGameEvent
    {
        public int TurnIndex { get; }
        public Guid PlayerId { get; }

        public TurnChangeEvent(int turnIndex, Guid playerId)
        {
            TurnIndex = turnIndex;
            PlayerId = playerId;
        }
    }
}