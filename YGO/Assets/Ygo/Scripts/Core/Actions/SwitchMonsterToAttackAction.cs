using System;
using Ygo.Core.Abstract;
using Ygo.Core.Actions.Abstract;

namespace Ygo.Core.Actions
{
    public class SwitchMonsterToAttackAction : BaseCardAction
    {
        public override string ActionName => "Switch to Attack";

        public SwitchMonsterToAttackAction(GameState gameState, Guid playerId, ICardInstance card) 
            : base(gameState, playerId, card)
        {
        }
        
        public override void Execute()
        {
            GameState.DoSwitchMonsterToAttack(PlayerId, Card);
        }
    }
}