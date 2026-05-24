using System.Collections.Generic;
using Ygo.Scripts.Core.Enum;
using Ygo.Scripts.Core.Event.Base;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Enum;

namespace Ygo.Scripts.Core.Event
{
    public class SelectPlaceEvent : IEvent
    {
        public byte Player { get; }
        public PointOfView PointOfView { get; }
        public bool CanCancel { get; }
        public uint Amount { get; }
        public IReadOnlyList<FieldZones> Choices { get; }

        public SelectPlaceEvent(byte player, PointOfView pointOfView, bool canCancel, uint amount, IReadOnlyList<FieldZones> choices)
        {
            Player = player;
            PointOfView = pointOfView;
            CanCancel = canCancel;
            Amount = amount;
            Choices = choices;
        }
    }
}