using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Ygo.Core.Abstract;
using Ygo.Data;
using Random = System.Random;

namespace Ygo.Core
{
    public class GameHandler
    {
        private const int StartingPLayerIndex = 0;
        private const int StartingPlayerHand = 5;
        private const int StartingLifePoints = 8000;

        public GameState GameState { get; private set; }
        
        public void Setup(ICardRepository repo)
        {
            GameState = new GameState();

            var turnContext = new TurnContext(new List<PlayerContext>()
            {
                CreatePlayer(repo),
                CreatePlayer(repo)
            });
            
            turnContext.Init(StartingPLayerIndex, StartingPlayerHand);
            
            GameState.Setup(turnContext);
        }

        public void Init()
        {
            GameState.Init();
        }

        private PlayerContext CreatePlayer(ICardRepository repo)
        {
            var cardsHandler = new CardsHandler();
            cardsHandler.Setup(repo);
            cardsHandler.ShuffleDeck();

            var player = new PlayerContext(cardsHandler, StartingLifePoints, true);
            return player;
        }
    }
}