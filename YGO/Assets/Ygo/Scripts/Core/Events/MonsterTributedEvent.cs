using System;
using Ygo.Core.Abstract;
using Ygo.Core.Board.Abstract;
using Ygo.Core.Events.Abstract;

namespace Ygo.Core.Events
{
    public class MonsterTributedEvent : IGameEvent
    {
        public Guid OwnerId { get; }
        public IBoardZone Zone { get; }
        public ICardInstance Card { get; }
        
        public MonsterTributedEvent(Guid ownerId, IBoardZone zone, ICardInstance card)
        {
            OwnerId = ownerId;
            Zone = zone;
            Card = card;
        }
    }
}