using System;
using System.Collections.Generic;
using Ygo.Core.Abstract;
using Ygo.Core.Events.Abstract;

namespace Ygo.Core.Events
{
    public class PlayerDeckUpdateEvent : IGameEvent
    {
        public IList<ICardInstance> Deck { get; }
        public Guid PlayerId { get; }
        
        public PlayerDeckUpdateEvent(IList<ICardInstance> deck, Guid playerId)
        {
            Deck = deck;
            PlayerId = playerId;
        }
    }
}