using System;
using Ygo.Core.Actions.Abstract;

namespace Ygo.Core.Actions
{
    public class DrawCardAction : IGameAction
    {
        private Guid _playerId;
        private GameState _gameState;
        
        public DrawCardAction(GameState gameState, Guid playerId)
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