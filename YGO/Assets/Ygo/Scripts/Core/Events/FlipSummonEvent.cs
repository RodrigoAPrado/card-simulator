using System;
using Ygo.Core.Abstract;
using Ygo.Core.Board.Abstract;
using Ygo.Core.Events.Abstract;

namespace Ygo.Core.Events
{
    public class FlipSummonEvent : IGameEvent
    {
        public Guid PlayerId { get; }
        public ICardInstance Card { get; }
        public FlipSummonEvent(Guid playerId, ICardInstance card)
        {
            PlayerId = playerId;
            Card = card;
        }
    }
}