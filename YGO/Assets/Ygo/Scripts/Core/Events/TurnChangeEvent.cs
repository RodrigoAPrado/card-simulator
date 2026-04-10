using System;
using Ygo.Core.Events.Abstract;

namespace Ygo.Core.Events
{
    public class TurnChangeEvent : IGameEvent
    {
        public int TurnIndex { get; }
        public Guid TurnPlayer { get; }

        public TurnChangeEvent(int turnIndex, Guid turnPlayer)
        {
            TurnIndex = turnIndex;
            TurnPlayer = turnPlayer;
        }
    }
}