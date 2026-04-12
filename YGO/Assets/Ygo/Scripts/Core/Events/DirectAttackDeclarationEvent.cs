using System;
using Ygo.Core.Abstract;
using Ygo.Core.Events.Abstract;

namespace Ygo.Core.Events
{
    public class DirectAttackDeclarationEvent : IGameEvent
    {
        public Guid PlayerId { get; }
        public ICardInstance Attacker { get; }

        public DirectAttackDeclarationEvent(Guid playerId, ICardInstance attacker)
        {
            PlayerId = playerId;
            Attacker = attacker;
        }
    }
}