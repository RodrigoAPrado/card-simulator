using System;
using Ygo.Core.Abstract;
using Ygo.Core.Actions.Abstract;

namespace Ygo.Core.Actions
{
    public class FlipSummonAction : BaseCardAction
    {
        public override string ActionName => "Flip Summon";
        
        public FlipSummonAction(GameState gameState, Guid playerId, ICardInstance card) 
            : base(gameState, playerId, card)
        {
        }

        public override void Execute()
        {
            GameState.DoFlipSummon(PlayerId, Card);
        }
    }
}