using System;
using Ygo.Core.Abstract;
using Ygo.Core.Events.Abstract;

namespace Ygo.Core.Events
{
    public class SwitchMonsterToAttackEvent : IGameEvent
    {
        public Guid PlayerId { get; }
        public ICardInstance Card { get; }
        public SwitchMonsterToAttackEvent(Guid playerId, ICardInstance card)
        {
            PlayerId = playerId;
            Card = card;
        }
    }
}