using System;
using Ygo.Core.Abstract;
using Ygo.Core.Actions.Abstract;

namespace Ygo.Core.Actions
{
    public class SwitchMonsterToDefenseAction : BaseCardAction
    {
        public override string ActionName => "Switch to Defense";
        
        public SwitchMonsterToDefenseAction(GameState gameState, Guid playerId, ICardInstance card) 
            : base(gameState, playerId, card)
        {
        }

        public override void Execute()
        {
            GameState.DoSwitchMonsterToDefense(PlayerId, Card);
        }
    }
}