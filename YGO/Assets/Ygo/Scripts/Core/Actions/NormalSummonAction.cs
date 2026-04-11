using System;
using Ygo.Core.Abstract;
using Ygo.Core.Actions.Abstract;

namespace Ygo.Core.Actions
{
    public class NormalSummonAction : IGameAction
    {
        public string ActionName => "Normal Summon";
        private readonly Guid _playerId;
        private readonly ICardInstance _card;
        private readonly GameState _gameState;

        public NormalSummonAction(GameState gameState, Guid playerId, ICardInstance card)
        {
            _gameState = gameState;
            _playerId = playerId;
            _card = card;
        }

        public void Execute()
        {
            _gameState.TryNormalSummon(_playerId, _card);
        }
    }
}