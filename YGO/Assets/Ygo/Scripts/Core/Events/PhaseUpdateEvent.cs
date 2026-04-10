using Ygo.Core.Events.Abstract;

namespace Ygo.Core.Events
{
    public class PhaseUpdateEvent : IGameEvent
    {
        public string PhaseName { get; }
        
        public PhaseUpdateEvent(string phaseName)
        {
            PhaseName = phaseName;
        }
    }
}