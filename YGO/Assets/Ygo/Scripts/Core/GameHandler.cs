using System;
using System.Collections.Generic;
using Ygo.Core.Abstract;
using Ygo.Core.Board;
using Ygo.Core.Board.Abstract;
using Ygo.Core.Board.Validator;
using Ygo.Core.Commands;
using Ygo.Core.Events;
using Ygo.Core.Interaction.Abstract;
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
        private IInteractionState _currentInteractionState;
        
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
            
            GameState.Setup(_turnContext, GameEventBus);
            RegisterHandlers();
        }

        private void RegisterHandlers()
        {
            GameCommandBus.RegisterHandler<MainDeckClickCommand>(MainDeckClickHandler);
            GameCommandBus.RegisterHandler<CardInHandClickCommand>(CardInHandClickHandler);
            GameCommandBus.RegisterHandler<ActionExecutionCommand>(ActionExecutionHandler);
            GameCommandBus.RegisterHandler<ZoneClickCommand>(ZoneClickHandler);
        }

        public void Init()
        {
            GameEventBus.Publish(new PointOfViewUpdateEvent(
                _turnContext.PointOfViewPlayer.Id, 
                _turnContext.OpponentPlayer.Id));
            GameEventBus.Publish(new PlayerInfoUpdateEvent(
                GameState.TurnContext.PointOfViewPlayer.PlayerName, 
                GameState.TurnContext.PointOfViewPlayer.CurrentLifePoints,
                GameState.TurnContext.OpponentPlayer.PlayerName, 
                GameState.TurnContext.OpponentPlayer.CurrentLifePoints));
            GameEventBus.Publish(new CardDrawnEvent(_turnContext.PointOfViewPlayer.Id));
            GameEventBus.Publish(new CardDrawnEvent(_turnContext.OpponentPlayer.Id));
            GameState.InitGame();
        }

        public void SetInteractionState(IInteractionState currentInteractionState)
        {
            if(_currentInteractionState != null)
                throw new InvalidOperationException("Cannot change interaction state while already set.");
            ;
            _currentInteractionState = currentInteractionState;
        }

        public void ClearInteractionState()
        {
            if(_currentInteractionState == null)
                throw new InvalidOperationException("Cannot change interaction state while not set.");
            _currentInteractionState = null;
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
            if (_currentInteractionState != null)
            {
                _currentInteractionState.Handle(c);
                return;
            }
            
            var response = GameState.ClickedOnMainDeck(c.PlayerId);
            if (response.Fail)
            {
                GameEventBus.Publish(new CommandDeniedEvent(CommandType.MainDeckCLicked, response.ActionState));
            }
        }

        private void CardInHandClickHandler(CardInHandClickCommand c)
        {
            if (_currentInteractionState != null)
            {
                _currentInteractionState.Handle(c);
                return;
            }
            
            var response = GameState.ClickCardInHand(c.PlayerId, c.Card);
            if (response.Fail)
            {
                GameEventBus.Publish(new CommandDeniedEvent(CommandType.MainDeckCLicked, response.ActionState));
            }
        }

        private void ZoneClickHandler(ZoneClickCommand c) {
            if (_currentInteractionState != null)
            {
                _currentInteractionState.Handle(c);
                return;
            }
            //GameState.ExecuteAction(c.Action);
        }

        private void ActionExecutionHandler(ActionExecutionCommand c)
        {
            if (_currentInteractionState != null)
            {
                _currentInteractionState.Handle(c);
                return;
            }
            GameState.ExecuteAction(c.Action);
        }
    }
}