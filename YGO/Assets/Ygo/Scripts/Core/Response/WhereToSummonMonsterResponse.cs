using System.Collections.Generic;
using Ygo.Core.Board.Abstract;

namespace Ygo.Core.Response
{
    public class WhereToSummonMonsterResponse
    {
        public bool CanSummon => AvailableZones is { Count: > 0 };
        public IList<IBoardZone> AvailableZones { get; }
        public WhereToSummonMonsterResponse(IList<IBoardZone> availableZones)
        {
            AvailableZones = availableZones;
        }
    }
}