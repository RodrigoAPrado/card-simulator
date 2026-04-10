using System.Collections.Generic;
using Ygo.Core.Abstract;
using Ygo.Core.Events.Abstract;

namespace Ygo.Core.Events
{
    public class PlayerDeckUpdateEvent : IGameEvent
    {
        public IList<ICardInstance> Deck { get; }
        public bool Pov { get; }
        
        public PlayerDeckUpdateEvent(IList<ICardInstance> deck, bool pov)
        {
            Deck = deck;
            Pov = pov;
        }
    }
}