using Ygo.Core.Events.Abstract;

namespace Ygo.Core.Events
{
    public class PlayerInfoUpdateEvent : IGameEvent
    {
        public string PlayerName { get; }
        public int PlayerLifePoint { get; }
        public string OpponentName { get; }
        public int OpponentLifePoint { get; }
        
        public PlayerInfoUpdateEvent(
            string playerName, 
            int playerLifePoint, 
            string opponentName, 
            int opponentLifePoint
            )
        {
            PlayerName = playerName;
            PlayerLifePoint = playerLifePoint;
            OpponentName = opponentName;
            OpponentLifePoint = opponentLifePoint;
        }
    }
}