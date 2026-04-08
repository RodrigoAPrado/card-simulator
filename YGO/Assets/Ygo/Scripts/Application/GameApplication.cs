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

        public void SubscribeToPhaseChange(Action action)
        {
            _gameHandler.GameState.SubscribeToPhaseChange(action);
        }
        
        public void SubscribeToTurnChange(Action action)
        {
            _gameHandler.GameState.SubscribeToTurnChange(action);
        }

        public void SubscribeToPointOfViewChange(Action action)
        {
            _gameHandler.SubscribeToPointOfViewChange(action);
        }

        public void SubscribeToBattleUpdate(Action action)
        {
            _gameHandler.GameState.SubscribeToBattleUpdate(action);
        }
    }
}