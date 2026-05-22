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
        public byte Player { get; }

        public SelectChainEvent(IReadOnlyList<CardModel> cards, bool canCancel, PointOfView pointOfView, byte player)
        {
            Cards = cards;
            CanCancel = canCancel;
            PointOfView = pointOfView;
            Player = player;
        }
    }
}