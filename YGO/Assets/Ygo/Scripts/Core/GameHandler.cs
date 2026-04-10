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
            GameState = new GameState(this);
            _validators = BuildZoneValidators();

            _turnContext = new TurnContext(new List<PlayerContext>()
            {
                CreatePlayer(repo, "Player1"),
                CreatePlayer(repo, "Player2")
            });
            
            _turnContext.Init(StartingPLayerIndex, StartingPlayerHand);
            
            GameState.Setup(_turnContext);
            RegisterHandlers();
        }

        private void RegisterHandlers()
        {
            GameCommandBus.RegisterHandler<MainDeckClickCommand>(MainDeckClickHandler);
        }

        public void Init()
        {
            GameState.Init();
            GameEventBus.Publish(new PlayerHandUpdateEvent(_turnContext.PointOfViewPlayer.CardsHandler.PlayerHand, true));
            GameEventBus.Publish(new PlayerHandUpdateEvent(_turnContext.OpponentPlayer.CardsHandler.PlayerHand, false));
            GameEventBus.Publish(new PlayerFieldUpdateEvent(_turnContext.PointOfViewPlayer.BoardHandler.MonsterZones, true));
            GameEventBus.Publish(new PlayerFieldUpdateEvent(_turnContext.OpponentPlayer.BoardHandler.MonsterZones, false));
            GameEventBus.Publish(new PhaseUpdateEvent(GameState.CurrentPhase.Name));
            GameEventBus.Publish(new PlayerInfoUpdateEvent(
                GameState.TurnContext.PointOfViewPlayer.PlayerName, 
                GameState.TurnContext.PointOfViewPlayer.CurrentLifePoints,
                GameState.TurnContext.OpponentPlayer.PlayerName, 
                GameState.TurnContext.OpponentPlayer.CurrentLifePoints));
            GameEventBus.Publish(new TurnChangeEvent(GameState.TurnContext.CurrentTurn));
            GameEventBus.Publish(new PlayerDeckUpdateEvent(_turnContext.PointOfViewPlayer.CardsHandler.MainDeck, true));
            GameEventBus.Publish(new PlayerDeckUpdateEvent(_turnContext.OpponentPlayer.CardsHandler.MainDeck, false));
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

        private void MainDeckClickHandler(MainDeckClickCommand c)
        {
            if (GameState.CurrentPhase is not DrawPhase)
            {
                GameEventBus.Publish(new CommandDeniedEvent(CommandType.MainDeckCLicked, DenialReason.NotInDrawPhase));
                return;
            }

            GameState.CurrentPhase.DrawFromDeck();
            if (c.PoV)
            {
                GameEventBus.Publish(new PlayerDeckUpdateEvent(_turnContext.PointOfViewPlayer.CardsHandler.MainDeck, true));
                GameEventBus.Publish(new PlayerHandUpdateEvent(_turnContext.PointOfViewPlayer.CardsHandler.PlayerHand, true));
            }
            else
            {
                GameEventBus.Publish(new PlayerDeckUpdateEvent(_turnContext.OpponentPlayer.CardsHandler.MainDeck, false));
                GameEventBus.Publish(new PlayerHandUpdateEvent(_turnContext.OpponentPlayer.CardsHandler.PlayerHand, false));
            }
        }
    }
}