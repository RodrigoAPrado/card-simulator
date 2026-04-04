using System;
using System.Collections.Generic;
using Ygo.Core;
using Ygo.Core.Abstract;
using Ygo.Core.Phases.Abstract;

namespace Ygo.Application
{
    public class GameApplication
    {
        public IList<ICardInstance> PlayerHand => _gameState.CardsHandler.CardsDrawn;
        public IList<ICardInstance> Deck => _gameState.CardsHandler.Deck;
        public IGamePhase CurrentPhase => _gameState.Phases.CurrentPhase;
        private readonly ICardRepository _cardRepository;
        private GameState _gameState;
        
        public GameApplication(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public void InitializeGame()
        {
            _gameState = new GameState();
            _gameState.Setup(_cardRepository);
            _gameState.Init();
        }

        public void ShuffleDeck()
        {
            _gameState.ShuffleDeck();
        }

        public void DrawInitialHand()
        {
            DrawCard(5);
        }
        
        public void DrawCard()
        {
            DrawCard(1);
        }

        private void DrawCard(int amount)
        {
            _gameState.DrawCard(amount);
        }

        public bool DrawFromDeck()
        {
            return CurrentPhase.DrawFromDeck();
        }

        public void SubscribeToPhaseChange(Action action)
        {
            _gameState.Phases.SubscribeToPhaseChange(action);
        }

        public void UnsubscribeToPhaseChange(Action action)
        {
            _gameState.Phases.UnsubscribeToPhaseChange(action);
        }
    }
}