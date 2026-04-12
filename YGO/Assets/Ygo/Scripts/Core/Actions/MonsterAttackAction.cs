using System;
using Ygo.Core.Abstract;
using Ygo.Core.Actions.Abstract;

namespace Ygo.Core.Actions
{
    public class MonsterAttackAction : BaseCardAction
    {
        public override string ActionName => "Attack";
        
        public MonsterAttackAction(GameState gameState, Guid playerId, ICardInstance card) 
            : base(gameState, playerId, card)
        {
        }

        public override void Execute()
        {
            
        }
    }
}