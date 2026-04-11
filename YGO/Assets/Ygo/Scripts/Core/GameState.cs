using System;
using System.Collections.Generic;
using Ygo.Core.Abstract;
using Ygo.Core.Enums;
using Ygo.Core.Events;
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
            if(CurrentPhase.CurrentStep == GameStep.ProceedToNextPhase)
                AdvancePhase();
        }

        public void TryNormalSummon(Guid playerId, ICardInstance card)
        {
            throw new NotImplementedException();
        }
        
        public void TryNormalSet(Guid playerId, ICardInstance card)
        {
            throw new NotImplementedException();
        }

        public void CancelAction()
        {
            _gameEventBus.Publish(new ActionCancelEvent());
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
                    query.Actions[0].Execute();
                    break;
                default:
                    _gameEventBus.Publish(new AvailableActionsEvent(query));
                    break;
            }
            
            return new CommandResponse(ActionState.Success);
        }
        
        private void HandleAdvancePhaseEffectPriority()
        {
            //Do Effects
            if (_currentEffects == null)
            {
                AdvancePhase();
            }
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