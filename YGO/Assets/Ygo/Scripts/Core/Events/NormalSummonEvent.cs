using System;
using Ygo.Core.Abstract;
using Ygo.Core.Board.Abstract;
using Ygo.Core.Events.Abstract;

namespace Ygo.Core.Events
{
    public class NormalSummonEvent : IGameEvent
    {
        public Guid PlayerId { get; }
        public ICardInstance Card { get; }
        public IBoardZone Zone { get; }
        
        public NormalSummonEvent(Guid playerId, ICardInstance card, IBoardZone zone)
        {
            PlayerId = playerId;
            Card = card;
            Zone = zone;
        }
    }
}