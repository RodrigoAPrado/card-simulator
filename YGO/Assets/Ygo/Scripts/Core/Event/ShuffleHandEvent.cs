using System.Collections.Generic;
using Ygo.Scripts.Core.Enum;
using Ygo.Scripts.Core.Event.Base;
using Ygo.Scripts.Core.Model;

namespace Ygo.Scripts.Core.Event
{
    public class ShuffleHandEvent : IEvent
    {
        public byte Player { get; }
        public PointOfView PointOfView { get; }
        public IReadOnlyList<CardModel> HandBefore { get; }
        public IReadOnlyList<CardModel> HandAfter { get; }

        public ShuffleHandEvent(
            byte player, 
            PointOfView pointOfView, 
            IReadOnlyList<CardModel> handBefore, 
            IReadOnlyList<CardModel> handAfter)
        {
            Player = player;
            PointOfView = pointOfView;
            HandBefore = handBefore;
            HandAfter = handAfter;
        }
    }
}