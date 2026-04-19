using System;
using System.Collections.Generic;
using Ygo.Core.Abstract;
using Ygo.Core.Actions;
using Ygo.Core.Actions.Abstract;
using Ygo.Core.Board;
using Ygo.Core.Board.Abstract;
using Ygo.Core.Board.Validator;
using Ygo.Core.Commands;
using Ygo.Core.Events;
using Ygo.Core.Interaction.Abstract;
using Ygo.Core.Phases;
using Ygo.Core.Response.Enum;

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
        
        public void Setup(ICardRepository cardRepo, ICardEffectRepository effectRepo)
        {
            GameCommandBus = new GameCommandBus();
            GameEventBus = new GameEventBus();
            GameState = new GameState(this);
            _validators = BuildZoneValidators();

            _turnContext = new TurnContext(new List<PlayerContext>()
            {
                CreatePlayer(cardRepo, "Player1"),
                CreatePlayer(cardRepo, "Player2")
            });
            
            _turnContext.Init(StartingPLayerIndex, StartingPlayerHand);
            var cardEffectLibrary = new GameCardEffectLibrary(effectRepo, _turnContext);
            
            GameState.Setup(_turnContext, GameEventBus, cardEffectLibrary);
            RegisterHandlers();
        }

        private void RegisterHandlers()
        {
            GameCommandBus.RegisterHandler<MainDeckClickCommand>(MainDeckClickHandler);
            GameCommandBus.RegisterHandler<CardInHandClickCommand>(CardInHandClickHandler);
            GameCommandBus.RegisterHandler<CardOnFieldClickCommand>(CardOnFieldClickHandler);
            GameCommandBus.RegisterHandler<ActionExecutionCommand>(ActionExecutionHandler);
            GameCommandBus.RegisterHandler<ZoneClickCommand>(ZoneClickHandler);
            GameCommandBus.RegisterHandler<NextPhaseClickCommand>(NextPhaseClickHandler);
            GameCommandBus.RegisterHandler<PlayerConfirmationCommand>(PlayerConfirmationCommandHandler);
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
            if(_currentInteractionState != null && _currentInteractionState != currentInteractionState)
                throw new InvalidOperationException("Cannot change interaction state while already set.");
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
            cardsHandler.AddCardToDeck(player.Id, repo.GetMainDeckCardById("55144522"), true);

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
            
            var response = GameState.ClickedOnMainDeck(c.RequesterId, c.OwnerId);
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
            
            var response = GameState.ClickCardInHand(c.RequesterId, c.OwnerId, c.Card);
            if (response.Fail)
            {
                GameEventBus.Publish(new CommandDeniedEvent(CommandType.CardInHandClicked, response.ActionState));
            }
        }

        private void CardOnFieldClickHandler(CardOnFieldClickCommand c)
        {
            if (_currentInteractionState != null)
            {
                _currentInteractionState.Handle(c);
                return;
            }
            
            var response = GameState.ClickCardOnField(c.RequesterId, c.OwnerId, c.Card);
            if (response.Fail)
            {
                GameEventBus.Publish(new CommandDeniedEvent(CommandType.CardOnFieldClicked, response.ActionState));
            }
        }

        private void ZoneClickHandler(ZoneClickCommand c) {
            if (_currentInteractionState != null)
            {
                _currentInteractionState.Handle(c);
                return;
            }
            var response = GameState.ClickZone(c.RequesterId, c.OwnerId, c.Zone);
            if (response.Fail)
            {
                GameEventBus.Publish(new CommandDeniedEvent(CommandType.ZoneClicked, response.ActionState));
            }
        }

        private void NextPhaseClickHandler(NextPhaseClickCommand c)
        {
            if (_currentInteractionState != null)
            {
                _currentInteractionState.Handle(c);
                return;
            }
            
            var response = GameState.ClickNextPhase(c.RequesterId);
            if (response.Fail)
            {
                GameEventBus.Publish(new CommandDeniedEvent(CommandType.NextPhaseClicked, response.ActionState));
            }
        }

        private void ActionExecutionHandler(ActionExecutionCommand c)
        {
            if (_currentInteractionState != null)
            {
                _currentInteractionState.Handle(c);
                return;
            }
            GameState.EnqueueActions(new List<IGameAction>{c.Action});
        }

        private void PlayerConfirmationCommandHandler(PlayerConfirmationCommand c)
        {
            if (_currentInteractionState != null)
            {
                _currentInteractionState.Handle(c);
                return;
            }
            GameEventBus.Publish(new CommandDeniedEvent(CommandType.PlayerConfirmation, ActionState.IncorrectStep));
        }
    }
}