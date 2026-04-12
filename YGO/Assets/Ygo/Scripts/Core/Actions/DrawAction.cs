using System;
using Ygo.Core.Actions.Abstract;

namespace Ygo.Core.Actions
{
    public class DrawAction : BaseGameAction
    {

        public override string ActionName => "Draw";
        private readonly Guid _playerId;
        
        public DrawAction(GameState gameState, Guid playerId) : base(gameState)
        {
            _playerId = playerId;
        }

        public override void Execute()
        {
            GameState.DrawFromDeck(_playerId);
        }
    }
}