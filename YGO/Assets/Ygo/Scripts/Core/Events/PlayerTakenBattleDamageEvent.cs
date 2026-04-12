using System;
using Ygo.Core.Events.Abstract;

namespace Ygo.Core.Events
{
    public class PlayerTakenBattleDamageEvent : IGameEvent
    {
        public Guid PlayerId { get; }
        public int Damage { get; }
        
        public PlayerTakenBattleDamageEvent(Guid playerId, int damage)
        {
            PlayerId = playerId;
            Damage = damage;
        }
    }
}