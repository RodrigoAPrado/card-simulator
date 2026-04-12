using System;
using Ygo.Core.Abstract;
using Ygo.Core.Board.Abstract;
using Ygo.Core.Events.Abstract;

namespace Ygo.Core.Events
{
    public class NormalSetEvent : NormalSummonEvent
    {
        public NormalSetEvent(Guid playerId, ICardInstance card, IBoardZone zone) : base(playerId, card, zone)
        {
        }
    }
}