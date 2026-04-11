using System;
using Ygo.Core.Actions.Abstract;

namespace Ygo.Core.Actions
{
    public class DrawAction : IGameAction
    {

        public string ActionName => "Draw";
        private readonly Guid _playerId;
        private readonly GameState _gameState;
        
        public DrawAction(GameState gameState, Guid playerId)
        {
            _gameState = gameState;
            _playerId = playerId;
        }

        public void Execute()
        {
            _gameState.DrawFromDeck(_playerId);
        }
    }
}