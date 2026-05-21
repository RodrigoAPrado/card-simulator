using System.Collections.Generic;
using System.Drawing;
using Ygo.Scripts.Core.Enum;
using Ygo.Scripts.Core.Event.Base;
using Ygo.Scripts.Core.Model;

namespace Ygo.Scripts.Core.Event
{
    public class DrawEvent : IEvent
    {
        public IReadOnlyList<CardModel> HandBefore { get; }
        public IReadOnlyList<CardModel> HandAfter { get; }
        public CardModel DrawnCard { get; }
        public PointOfView PointOfView { get; }

        public DrawEvent(
            IReadOnlyList<CardModel> handBefore, 
            IReadOnlyList<CardModel> handAfter, 
            CardModel drawCard,
            PointOfView pointOfView)
        {
            HandBefore = handBefore;
            HandAfter = handAfter;
            DrawnCard = drawCard;
            PointOfView = pointOfView;
        }
    }
}