using Ygo.Scripts.Core.Enum;
using Ygo.Scripts.Core.Event.Base;

namespace Ygo.Scripts.Core.Event
{
    public class DrawDeckEvent : IEvent
    {
        public int AmountBefore { get; }
        public int AmountAfter { get; }
        public PointOfView PointOfView { get; }

        public DrawDeckEvent(int amountBefore, int amountAfter, PointOfView pointOfView)
        {
            AmountBefore = amountBefore;
            AmountAfter = amountAfter;
            PointOfView = pointOfView;
        }
    }
}