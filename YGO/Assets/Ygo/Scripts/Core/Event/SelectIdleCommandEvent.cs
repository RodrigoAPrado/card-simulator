using System.Collections.Generic;
using Ygo.Scripts.Core.Enum;
using Ygo.Scripts.Core.Event.Base;
using Ygo.Scripts.Core.Model;

namespace Ygo.Scripts.Core.Event
{
    public class SelectIdleCommandEvent : IEvent
    {
        public byte Player { get; }
        public PointOfView PointOfView { get; }
        public int BattlePhaseIndex { get; }
        public bool HasBattlePhase => BattlePhaseIndex >= 0;
        public int EndPhaseIndex { get; }
        public bool HasEndPhase => EndPhaseIndex >= 0;
        public int ShuffleHandIndex { get; }
        public bool HasShuffleHand => ShuffleHandIndex >= 0;
        public IList<CardIdleCommandModel> CardCommands { get; }

        public SelectIdleCommandEvent(
            byte player, 
            PointOfView pointOfView, 
            int battlePhaseIndex, 
            int endPhaseIndex, 
            int shuffleHandIndex, 
            IList<CardIdleCommandModel> cardCommands)
        {
            Player = player;
            PointOfView = pointOfView;
            BattlePhaseIndex = battlePhaseIndex;
            EndPhaseIndex = endPhaseIndex;
            ShuffleHandIndex = shuffleHandIndex;
            CardCommands = cardCommands;
        }
    }
}