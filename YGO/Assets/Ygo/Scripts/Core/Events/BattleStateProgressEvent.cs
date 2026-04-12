using Ygo.Core.Abstract;
using Ygo.Core.Enums;
using Ygo.Core.Events.Abstract;

namespace Ygo.Core.Events
{
    public class BattleStateProgressEvent : IGameEvent
    {
        public ICardInstance Attacker { get; }
        public ICardInstance Defender { get; }
        public BattleStep BattleStep { get; }
        
        public BattleStateProgressEvent(ICardInstance attacker, ICardInstance defender, BattleStep battleStep)
        {
            Attacker = attacker;
            Defender = defender;
            BattleStep = battleStep;
        }
    }
}