using System.Collections.Generic;
using Ygo.Scripts.Core.Event.Base;
using Ygo.Scripts.Core.Model;

namespace Ygo.Scripts.Core.Event
{
    public class DrawEvent : IEvent
    {
        public IReadOnlyList<CardModel> HandBefore { get; }
        public IReadOnlyList<CardModel> HandAfter { get; }
        public CardModel DrawnCard { get; }

        public DrawEvent(
            IReadOnlyList<CardModel> handBefore, 
            IReadOnlyList<CardModel> handAfter, 
            CardModel drawCard)
        {
            HandBefore = handBefore;
            HandAfter = handAfter;
            DrawnCard = drawCard;
        }
    }
}