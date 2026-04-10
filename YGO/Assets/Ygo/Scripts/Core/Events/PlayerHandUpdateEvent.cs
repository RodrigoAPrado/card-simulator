using System.Collections.Generic;
using Ygo.Core.Abstract;
using Ygo.Core.Events.Abstract;

namespace Ygo.Core.Events
{
    public class PlayerHandUpdateEvent : IGameEvent
    {
        public IList<ICardInstance> Hand { get; }
        public bool Pov { get; }

        public PlayerHandUpdateEvent(IList<ICardInstance> hand, bool pov)
        {
            Hand = hand;
            Pov = pov;
        }
    }
}