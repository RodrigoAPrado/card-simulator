using System;
using Ygo.Core.Events.Abstract;

namespace Ygo.Core.Events
{
    public class PointOfViewUpdateEvent : IGameEvent
    {
        public Guid PointOfViewId { get; set; }
        public Guid OpponentId { get; set; }
        
        public PointOfViewUpdateEvent(Guid pointOfViewId, Guid opponentId)
        {
            PointOfViewId = pointOfViewId;
            OpponentId = opponentId;
        }
    }
}