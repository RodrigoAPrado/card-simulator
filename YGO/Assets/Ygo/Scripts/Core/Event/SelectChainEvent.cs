using System.Collections.Generic;
using Ygo.Scripts.Core.Enum;
using Ygo.Scripts.Core.Event.Base;
using Ygo.Scripts.Core.Model;

namespace Ygo.Scripts.Core.Event
{
    public class SelectChainEvent : IEvent
    {
        public IReadOnlyList<CardModel> Cards { get; }
        public bool CanCancel { get; }
        public PointOfView PointOfView { get; }

        public SelectChainEvent(IReadOnlyList<CardModel> cards, bool canCancel, PointOfView pointOfView)
        {
            Cards = cards;
            CanCancel = canCancel;
            PointOfView = pointOfView;
        }
    }
}