using System;
using Ygo.Core.Abstract;
using Ygo.Core.Events.Abstract;

namespace Ygo.Core.Events
{
    public class FlipEvent : IGameEvent
    {
        public Guid PlayerId { get; }
        public ICardInstance Card { get; }
        public FlipEvent(Guid playerId, ICardInstance card)
        {
            PlayerId = playerId;
            Card = card;
        }
    }
}