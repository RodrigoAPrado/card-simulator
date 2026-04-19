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
        public TurnContext TurnContext => _gameHandler.GameState.TurnContext;
        private readonly ICardRepository _cardRepository;
        private readonly ICardEffectRepository _cardEffectRepository;
        private GameHandler _gameHandler;
        
        public GameApplication(ICardRepository cardRepository, ICardEffectRepository cardEffectRepository)
        {
            _cardRepository = cardRepository;
            _cardEffectRepository = cardEffectRepository;
        }

        public void Setup()
        {
            _gameHandler = new GameHandler();
            _gameHandler.Setup(_cardRepository, _cardEffectRepository);
        }

        public void Init()
        {
            _gameHandler.Init();
        }
    }
}