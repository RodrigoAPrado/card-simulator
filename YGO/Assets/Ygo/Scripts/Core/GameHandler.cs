using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Ygo.Core.Abstract;
using Ygo.Core.Board;
using Ygo.Core.Board.Abstract;
using Ygo.Core.Board.Validator;
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
        private IDictionary<ZoneType, IPutCardInZoneValidator> _validators;
        
        public void Setup(ICardRepository repo)
        {
            GameState = new GameState();
            _validators = BuildZoneValidators();

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

            var boardHandler = new BoardHandler();
            boardHandler.Setup(_validators);

            var player = new PlayerContext(cardsHandler, boardHandler, StartingLifePoints, true);
            return player;
        }

        private Dictionary<ZoneType, IPutCardInZoneValidator> BuildZoneValidators()
        {
            return new Dictionary<ZoneType, IPutCardInZoneValidator>
            {
                { ZoneType.FieldZone, new PutFieldSpellInZoneValidator() },
                { ZoneType.MainMonsterZone, new PutMonsterInZoneValidator() },
                { ZoneType.SpellTrapZone, new PutSpellTrapInZoneValidator() }
            };
        }
    }
}