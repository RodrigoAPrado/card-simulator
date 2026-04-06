using System;
using Ygo.Core.Enums;

namespace Ygo.Core.Board.Abstract
{
    public enum ZonePosition
    {
        Unknown = 0,
        Field = 1,
        LeftMost = 2,
        LeftCenter = 3,
        MiddleCenter = 4,
        RightCenter = 5,
        RightMost = 6
    }

    public static class ZonePositionExtensions
    {
        public static CardLocation ToMonsterCardLocation(this ZonePosition zonePosition)
        {
            return zonePosition switch
            {
                ZonePosition.LeftMost => CardLocation.LeftMostMonsterZone,
                ZonePosition.LeftCenter => CardLocation.LeftCenterMonsterZone,
                ZonePosition.MiddleCenter => CardLocation.MiddleCenterMonsterZone,
                ZonePosition.RightCenter => CardLocation.RightCenterMonsterZone,
                ZonePosition.RightMost => CardLocation.RightMostMonsterZone,
                _ => throw new InvalidOperationException("Invalid Zone Position")
            };
        }
    } 
}