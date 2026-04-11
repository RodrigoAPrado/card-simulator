using System;
using System.Collections.Generic;
using Ygo.Core.Abstract;
using Ygo.Core.Actions.Abstract;
using Ygo.Core.Board.Abstract;
using Ygo.Core.Enums;
using Ygo.Core.Events;
using Ygo.Core.Interaction.Abstract;
using Ygo.Core.Phases;
using Ygo.Core.Phases.Abstract;
using Ygo.Core.Response;
using Ygo.Core.Response.Enum;

namespace Ygo.Core
{
    public class GameState
    {
        public TurnContext TurnContext { get; private set; }
        public IGamePhase CurrentPhase => _phases[_currentPhaseIndex];
        private List<IGamePhase> _phases;
        private int _currentPhaseIndex;
        private EffectPriorityContext _effectPriorityContext;
        private List<PlayerEffects> _effectPriority;
        private PlayerEffects _currentEffects;
        private int _currentPlayerEffectIndex;
        private GameEventBus _gameEventBus;
        private GameHandler _gameHandler;

        public GameState(GameHandler gameHandler)
        {
            _gameHandler = gameHandler;
        }
        
        public void Setup(TurnContext turnContext, GameEventBus gameEventBus)
        {
            TurnContext = turnContext;
            _gameEventBus = gameEventBus;
            _effectPriorityContext = new EffectPriorityContext(TurnContext);
            _effectPriority = new List<PlayerEffects>();
            _phases = new List<IGamePhase>
            {
                new DrawPhase(TurnContext, this),
                new StandbyPhase(TurnContext, this),
                new MainPhase1(TurnContext, this),
                new BattlePhase(TurnContext, this),
                new MainPhase2(TurnContext, this),
                new EndPhase(TurnContext, this)
            };
            _currentPhaseIndex = 0;
        }

        public CommandResponse ClickedOnMainDeck(Guid playerId)
        {
            return ProcessActionQuery(CurrentPhase.ClickedOnMainDeck(playerId));
        }

        public CommandResponse ClickCardInHand(Guid playerId, ICardInstance card)
        {
            return ProcessActionQuery(CurrentPhase.ClickedOnCardInHand(playerId, card));
        }

        public void InitGame()
        {
            StartTurn();
        }

        public void DrawFromDeck(Guid playerId)
        {
            var result = CurrentPhase.DrawCard(playerId);
            if (result.ActionState != ActionState.Success)
            {
                throw new InvalidOperationException("Invalid action!");
            }
            
            _gameEventBus.Publish(new CardDrawnEvent(playerId));
        }

        public void CheckNormalSummon(Guid playerId, ICardInstance card)
        {
            CurrentPhase.CheckNormalSummon(playerId, card);
        }
        
        public void CheckNormalSet(Guid playerId, ICardInstance card)
        {
            throw new NotImplementedException();
        }

        public void DoNormalSummon(Guid playerId, ICardInstance card, IBoardZone boardZone)
        {
            CurrentPhase.DoNormalSummon(playerId, card, boardZone);
            _gameEventBus.Publish(new NormalSummonEvent(playerId, card, boardZone));
        }

        public void CancelAction()
        {
            _gameEventBus.Publish(new ActionCancelEvent());
        }

        public void ExecuteAction(IGameAction action)
        {
            action.Execute();
            ResolveGameStep();
        }

        public void SetInteractionState(Guid playerId, IInteractionState interactionState)
        {
            _gameHandler.SetInteractionState(interactionState);
            _gameEventBus.Publish(new InteractionStateSetEvent(playerId, interactionState));
        }

        public void ClearInteractionState(Guid playerId)
        {
            _gameHandler.ClearInteractionState();
        }
        
        private void ResolveGameStep()
        {
            HandlePhaseProgression();
            HandleAdvancePhaseEffectPriority();
        }

        private CommandResponse ProcessActionQuery(ActionQuery query)
        {
            if (!query.Success)
            {
                return new CommandResponse(query.ActionState);
            }

            switch (query.Actions.Count)
            {
                case <= 0:
                    throw new InvalidOperationException("Invalid success with no Actions.");
                case 1:
                    ExecuteAction(query.Actions[0]);
                    break;
                default:
                    _gameEventBus.Publish(new AvailableActionsEvent(query));
                    break;
            }
            
            return new CommandResponse(ActionState.Success);
        }

        private void HandlePhaseProgression()
        {
            if(CurrentPhase.CurrentStep == GameStep.ProceedToNextPhase)
                AdvancePhase();
        }
        
        private void HandleAdvancePhaseEffectPriority()
        {
        }

        private void AdvancePhase()
        {
            while (true)
            {
                _gameEventBus.Publish(new PhaseEndEvent(CurrentPhase.Phase, TurnContext.CurrentTurnPlayer.Id));
                if (CurrentPhase.Phase == GamePhase.EndPhase)
                {
                    TurnChange();
                    return;
                }
                _currentPhaseIndex++;
                CurrentPhase.Init();
                _gameEventBus.Publish(new PhaseBeginEvent(CurrentPhase.Phase, TurnContext.CurrentTurnPlayer.Id));
                if (CurrentPhase.CurrentStep != GameStep.ProceedToNextPhase)
                    break;
            }
        }

        private void TurnChange()
        {
            TurnContext.AdvanceTurn();
            _gameEventBus.Publish(new TurnChangeEvent(TurnContext.CurrentTurn, TurnContext.CurrentTurnPlayer.Id));
            StartTurn();
        }
        
        private void StartTurn()
        {
            _currentPhaseIndex = 0;
            CurrentPhase.Init();
            _gameEventBus.Publish(new PhaseBeginEvent(CurrentPhase.Phase, TurnContext.CurrentTurnPlayer.Id));
        }
    }
}