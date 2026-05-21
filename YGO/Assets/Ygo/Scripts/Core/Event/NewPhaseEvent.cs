using Ygo.Scripts.Core.Event.Base;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Enum;

namespace Ygo.Scripts.Core.Event
{
    public class NewPhaseEvent : IEvent
    {
        public DuelPhase Phase { get; }

        public NewPhaseEvent(DuelPhase phase)
        {
            Phase = phase;
        }
    }
}