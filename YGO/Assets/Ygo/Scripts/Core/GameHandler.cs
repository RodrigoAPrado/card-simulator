using System.Collections.Generic;
using Ygo.Core.Abstract;
using Ygo.Core.Board;
using Ygo.Core.Board.Abstract;
using Ygo.Core.Board.Validator;
using Ygo.Core.Commands;
using Ygo.Core.Events;
using Ygo.Core.Phases;

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
            GameState = new GameState();
            _validators = BuildZoneValidators();

            _turnContext = new TurnContext(new List<PlayerContext>()
            {
                CreatePlayer(repo, "Player1"),
                CreatePlayer(repo, "Player2")
            });
            
            _turnContext.Init(StartingPLayerIndex, StartingPlayerHand);
            
            GameState.Setup(_turnContext, GameEventBus);
            RegisterHandlers();
        }

        private void RegisterHandlers()
        {
            GameCommandBus.RegisterHandler<MainDeckClickCommand>(MainDeckClickHandler);
            GameCommandBus.RegisterHandler<CardInHandClickCommand>(CardInHandClickHandler);
        }

        public void Init()
        {
            GameState.Init();
            GameEventBus.Publish(new PointOfViewUpdateEvent(
                _turnContext.PointOfViewPlayer.Id, 
                _turnContext.OpponentPlayer.Id));
            GameEventBus.Publish(new PlayerHandUpdateEvent(
                _turnContext.PointOfViewPlayer.CardsHandler.PlayerHand, 
                _turnContext.PointOfViewPlayer.Id));
            GameEventBus.Publish(new PlayerHandUpdateEvent(
                _turnContext.OpponentPlayer.CardsHandler.PlayerHand, 
                _turnContext.OpponentPlayer.Id));
            GameEventBus.Publish(new PlayerFieldUpdateEvent(
                _turnContext.PointOfViewPlayer.BoardHandler.MonsterZones, 
                _turnContext.PointOfViewPlayer.Id));
            GameEventBus.Publish(new PlayerFieldUpdateEvent(
                _turnContext.OpponentPlayer.BoardHandler.MonsterZones, 
                _turnContext.OpponentPlayer.Id));
            GameEventBus.Publish(new PlayerInfoUpdateEvent(
                GameState.TurnContext.PointOfViewPlayer.PlayerName, 
                GameState.TurnContext.PointOfViewPlayer.CurrentLifePoints,
                GameState.TurnContext.OpponentPlayer.PlayerName, 
                GameState.TurnContext.OpponentPlayer.CurrentLifePoints));
            GameEventBus.Publish(new TurnChangeEvent(GameState.TurnContext.CurrentTurn));
            GameEventBus.Publish(new PlayerDeckUpdateEvent(
                _turnContext.PointOfViewPlayer.CardsHandler.MainDeck, 
                _turnContext.PointOfViewPlayer.Id));
            GameEventBus.Publish(new PlayerDeckUpdateEvent(
                _turnContext.OpponentPlayer.CardsHandler.MainDeck, 
                _turnContext.OpponentPlayer.Id));
            GameEventBus.Publish(new PlayerShouldDrawEvent(_turnContext.CurrentTurnPlayer.Id));
        }

        private PlayerContext CreatePlayer(ICardRepository repo, string playerName)
        {
            var player = new PlayerContext(playerName, StartingLifePoints, true);
            
            var cardsHandler = new CardsHandler();
            cardsHandler.Setup(repo, player.Id);
            cardsHandler.ShuffleDeck();

            var boardHandler = new BoardHandler();
            boardHandler.Setup(_validators);
            player.Setup(cardsHandler, boardHandler);
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

        private void MainDeckClickHandler(MainDeckClickCommand c)
        {
            var response = GameState.TryDrawFromDeck(c.PlayerId);

            if (response.Fail)
            {
                GameEventBus.Publish(new CommandDeniedEvent(CommandType.MainDeckCLicked, response.GameStateResult));
            }
        }

        private void CardInHandClickHandler(CardInHandClickCommand c)
        {
            var response = GameState.ClickCardInHand(c.Card);
            if (response.Fail)
            {
                GameEventBus.Publish(new CommandDeniedEvent(CommandType.MainDeckCLicked, response.GameStateResult));
            }
        }
    }
}