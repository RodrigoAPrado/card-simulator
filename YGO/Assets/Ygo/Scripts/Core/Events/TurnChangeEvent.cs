using Ygo.Core.Events.Abstract;

namespace Ygo.Core.Events
{
    public class TurnChangeEvent : IGameEvent
    {
        public int TurnIndex { get; }

        public TurnChangeEvent(int turnIndex)
        {
            TurnIndex = turnIndex;
        }
    }
}