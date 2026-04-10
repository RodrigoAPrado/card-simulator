using System;
using System.Collections.Generic;
using Ygo.Core.Board.Abstract;
using Ygo.Core.Events.Abstract;

namespace Ygo.Core.Events
{
    public class PlayerFieldUpdateEvent : IGameEvent
    {
        public IList<IBoardZone> Board { get; }
        public Guid PlayerId { get; }

        public PlayerFieldUpdateEvent(IList<IBoardZone> board, Guid playerId)
        {
            Board = board;
            PlayerId = playerId;
        }
    }
}