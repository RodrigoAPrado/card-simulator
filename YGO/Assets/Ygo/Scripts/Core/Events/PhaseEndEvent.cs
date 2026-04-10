using System;
using Ygo.Core.Enums;
using Ygo.Core.Events.Abstract;

namespace Ygo.Core.Events
{
    public class PhaseEndEvent : IGameEvent
    {
        public Guid TurnPlayer { get; }
        public GamePhase Phase { get; }
        public PhaseEndEvent(GamePhase phase, Guid turnPlayer)
        {
            Phase = phase;
            TurnPlayer = turnPlayer;
        }
    }
}