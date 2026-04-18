using System;
using Ygo.Core.Abstract;
using Ygo.Core.Actions.Abstract;

namespace Ygo.Core.Actions
{
    public class TributeSummonAction : BaseCardAction
    {
        public override string ActionName => "Tribute Summon";
        
        public TributeSummonAction(GameState gameState, Guid playerId, ICardInstance card) 
            : base(gameState, playerId, card)
        {
        }
        public override void Execute()
        {
            GameState.ConfirmTributeSummon(PlayerId, Card);
        }
    }
}