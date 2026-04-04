using System.Collections.Generic;
using Ygo.Core;
using Ygo.Core.Abstract;

namespace Ygo.Scripts.Application
{
    public class GameApplication
    {
        private ICardRepository _cardRepository;
        private IGameState _gameState { get; set; }
        
        public GameApplication(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public void InitializeGame()
        {
            _gameState = new GameState();
        }

        public IList<ICardInstance> DrawCards()
        {
            _gameState.DrawCards(_cardRepository, 5);
            return _gameState.CardsDrawn;
        }
    }
}