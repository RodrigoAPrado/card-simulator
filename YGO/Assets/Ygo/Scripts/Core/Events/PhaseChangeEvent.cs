using Ygo.Core.Enums;
using Ygo.Core.Events.Abstract;

namespace Ygo.Core.Events
{
    public class PhaseChangeEvent : IGameEvent
    {
        public GamePhase Phase { get; }
        public PhaseChangeEvent(GamePhase phase)
        {
            Phase = phase;
        }
    }
}