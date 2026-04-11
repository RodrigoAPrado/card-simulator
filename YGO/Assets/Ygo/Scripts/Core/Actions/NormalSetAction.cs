using System;
using Ygo.Core.Abstract;
using Ygo.Core.Actions.Abstract;

namespace Ygo.Core.Actions
{
    public class NormalSetAction : IGameAction
    {
        public string ActionName => "Normal Set";
        private readonly Guid _playerId;
        private readonly ICardInstance _card;
        private readonly GameState _gameState;

        public NormalSetAction(GameState gameState, Guid playerId, ICardInstance card)
        {
            _gameState = gameState;
            _playerId = playerId;
            _card = card;
        }

        public void Execute()
        {
            _gameState.CheckNormalSet(_playerId, _card);
        }
    }
}