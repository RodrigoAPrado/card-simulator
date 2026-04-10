using System;
using System.Collections.Generic;
using Ygo.Core;
using Ygo.Core.Abstract;
using Ygo.Core.Phases.Abstract;

namespace Ygo.Application
{
    public class GameApplication
    {
        public PlayerContext PointOfViewPlayer => _gameHandler.GameState.TurnContext.PointOfViewPlayer;
        public PlayerContext OpponentPlayer => _gameHandler.GameState.TurnContext.OpponentPlayer;
        public GameCommandBus GameCommandBus => _gameHandler.GameCommandBus;
        public GameEventBus GameEventBus => _gameHandler.GameEventBus;
        public IGamePhase CurrentPhase => _gameHandler.GameState.CurrentPhase;
        public int CurrentTurn => _gameHandler.GameState.TurnContext.CurrentTurn;
        private readonly ICardRepository _cardRepository;
        private GameHandler _gameHandler;
        
        public GameApplication(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public void InitializeGame()
        {
            _gameHandler = new GameHandler();
            _gameHandler.Setup(_cardRepository);
            _gameHandler.Init();
        }

        public bool DrawFromDeck()
        {
            return CurrentPhase.DrawFromDeck();
        }
    }
}