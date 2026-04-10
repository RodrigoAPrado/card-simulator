using System;
using System.Collections.Generic;
using Ygo.Core.Abstract;
using Ygo.Core.Board;
using Ygo.Core.Board.Abstract;
using Ygo.Core.Board.Validator;

namespace Ygo.Core
{
    public class GameHandler
    {
        private const int StartingPLayerIndex = 0;
        private const int StartingPlayerHand = 5;
        private const int StartingLifePoints = 8000;

        public GameState GameState { get; private set; }
        public GameCommandBus GameCommandBus { get; private set; }
        public GameEventBus GameEventBus { get; private set; }
        private IDictionary<ZoneType, IPutCardInZoneValidator> _validators;
        private TurnContext _turnContext;
        
        public void Setup(ICardRepository repo)
        {
            GameCommandBus = new GameCommandBus();
            GameEventBus = new GameEventBus();
            GameState = new GameState(this);
            _validators = BuildZoneValidators();

            _turnContext = new TurnContext(new List<PlayerContext>()
            {
                CreatePlayer(repo, "Player1"),
                CreatePlayer(repo, "Player2")
            });
            
            _turnContext.Init(StartingPLayerIndex, StartingPlayerHand);
            
            GameState.Setup(_turnContext);
        }

        public void Init()
        {
            GameState.Init();
        }

        private PlayerContext CreatePlayer(ICardRepository repo, string playerName)
        {
            var cardsHandler = new CardsHandler();
            cardsHandler.Setup(repo);
            cardsHandler.ShuffleDeck();

            var boardHandler = new BoardHandler();
            boardHandler.Setup(_validators);

            var player = new PlayerContext(playerName, cardsHandler, boardHandler, StartingLifePoints, true);
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