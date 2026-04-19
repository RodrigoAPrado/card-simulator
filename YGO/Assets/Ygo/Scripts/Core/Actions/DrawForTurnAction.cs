using System;
using Ygo.Core.Actions.Abstract;

namespace Ygo.Core.Actions
{
    public class DrawForTurnAction : BaseGameAction
    {

        public override string ActionName => "Draw";
        private readonly Guid _playerId;
        
        public DrawForTurnAction(GameState gameState, Guid playerId) : base(gameState)
        {
            _playerId = playerId;
        }

        public override void Execute()
        {
            GameState.DrawForTurn(_playerId);
        }
    }
}