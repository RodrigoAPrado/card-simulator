using System;
using Ygo.Core.Abstract;
using Ygo.Core.Events.Abstract;

namespace Ygo.Core.Events
{
    public class CardDestroyedByBattleEvent : IGameEvent
    {
        public Guid PlayerId { get; }
        public ICardInstance Card { get; }
        public CardDestroyedByBattleEvent(Guid playerId, ICardInstance card)
        {
            PlayerId = playerId;
            Card = card;
        }
    }
}