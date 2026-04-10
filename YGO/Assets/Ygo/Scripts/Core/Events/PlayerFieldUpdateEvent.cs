using System.Collections.Generic;
using Ygo.Core.Abstract;
using Ygo.Core.Board.Abstract;
using Ygo.Core.Events.Abstract;

namespace Ygo.Core.Events
{
    public class PlayerFieldUpdateEvent : IGameEvent
    {
        public IList<IBoardZone> Board { get; }
        public bool Pov { get; }

        public PlayerFieldUpdateEvent(IList<IBoardZone> board, bool pov)
        {
            Board = board;
            Pov = pov;
        }
    }
}