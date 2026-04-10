using System;
using Ygo.Core.Enums;
using Ygo.Core.Events.Abstract;

namespace Ygo.Core.Events
{
    public class PhaseBeginEvent : IGameEvent
    {
        public GamePhase Phase { get; }
     
        public Guid TurnPlayer { get; }
        public PhaseBeginEvent(GamePhase phase, Guid turnPlayer)
        {
            Phase = phase;
            TurnPlayer = turnPlayer;
        }
    }
}