using System;
using Ygo.Core.Abstract;
using Ygo.Core.Actions.Abstract;

namespace Ygo.Core.Actions
{
    public class NormalSummonAction : BaseCardAction
    {
        public override string ActionName => "Normal Summon";

        public NormalSummonAction(GameState gameState, Guid playerId, ICardInstance card) 
            : base(gameState, playerId, card)
        {
        }

        public override void Execute()
        {
            GameState.CheckNormalSummon(PlayerId, Card, false);
        }
    }
}