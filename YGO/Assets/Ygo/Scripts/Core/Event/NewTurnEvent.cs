using Ygo.Scripts.Core.Enum;
using Ygo.Scripts.Core.Event.Base;

namespace Ygo.Scripts.Core.Event
{
    public class NewTurnEvent : IEvent
    {
        public byte TurnNumber { get; }
        public PointOfView TurnPointOfView { get; }

        public NewTurnEvent(byte turnNumber, PointOfView turnPointOfView)
        {
            TurnNumber = turnNumber;
            TurnPointOfView = turnPointOfView;
        }
    }
}