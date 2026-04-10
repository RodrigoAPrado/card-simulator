using Ygo.Core.Enums;
using Ygo.Core.Events.Abstract;

namespace Ygo.Core.Events
{
    public class PhaseEndEvent : IGameEvent
    {
        public GamePhase Phase { get; }
        public PhaseEndEvent(GamePhase phase)
        {
            Phase = phase;
        }
    }
}