using System;
using Ygo.Core.Abstract;
using Ygo.Core.Events.Abstract;

namespace Ygo.Core.Events
{
    public class AttackDeclarationEvent : IGameEvent
    {
        public Guid PlayerId { get; }
        public ICardInstance Attacker { get; }
        public ICardInstance Defender { get; }

        public AttackDeclarationEvent(Guid playerId, ICardInstance attacker, ICardInstance defender)
        {
            PlayerId = playerId;
            Attacker = attacker;
            Defender = defender;
        }
    }
}