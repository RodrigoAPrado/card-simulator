using System;
using System.Collections.Generic;
using Ygo.Core.Abstract;
using Ygo.Core.Events.Abstract;

namespace Ygo.Core.Events
{
    public class CardDrawnEvent : IGameEvent
    {
        public Guid PlayerId { get; }
        
        public CardDrawnEvent(Guid playerId)
        {
            PlayerId = playerId;
        }
    }
}