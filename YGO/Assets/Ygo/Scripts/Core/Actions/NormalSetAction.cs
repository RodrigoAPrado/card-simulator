using System;
using Ygo.Core.Abstract;
using Ygo.Core.Actions.Abstract;

namespace Ygo.Core.Actions
{
    public class NormalSetAction : BaseCardAction
    {
        public override string ActionName => "Normal Set";

        public NormalSetAction(GameState gameState, Guid playerId, ICardInstance card) : base(gameState, playerId, card)
        {
        }

        public override void Execute()
        {
            GameState.CheckNormalSet(PlayerId, Card, false);
        }
    }
}