using Ygo.Core.Enums;
using Ygo.Core.Events.Abstract;

namespace Ygo.Core.Events
{
    public class PhaseBeginEvent : IGameEvent
    {
        public GamePhase Phase { get; }
        public PhaseBeginEvent(GamePhase phase)
        {
            Phase = phase;
        }
    }
}