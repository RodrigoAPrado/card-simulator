using System;
using Ygo.Core.Abstract;
using Ygo.Core.Actions.Abstract;

namespace Ygo.Core.Actions
{
    public class TributeSetAction : BaseCardAction
    {
        public override string ActionName => "Tribute Set";
        
        public TributeSetAction(GameState gameState, Guid playerId, ICardInstance card) 
            : base(gameState, playerId, card)
        {
        }
        public override void Execute()
        {
            GameState.RequestTributeSummon(PlayerId, Card, true);
        }
    }
}