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
                new DrawPhase(TurnContext),
                new StandbyPhase(TurnContext),
                new MainPhase1(TurnContext),
                new BattlePhase(TurnContext),
                new MainPhase2(TurnContext),
                new EndPhase(TurnContext)
            };
            _currentPhaseIndex = 0;
        }

        public void Init()
        {
            CurrentPhase.Init();
            _gameEventBus.Publish(new PhaseBeginEvent(CurrentPhase.Phase));
        }

        public CommandResponse TryDrawFromDeck(Guid playerId)
        {
            if (CurrentPhase.Phase != GamePhase.DrawPhase)
                return new CommandResponse(GameStateResult.IncorrectPhase);
            if (TurnContext.CurrentTurnPlayer.Id != playerId)
                return new CommandResponse(GameStateResult.IncorrectPlayer);
            
            var result = CurrentPhase.DrawFromDeck();
            if (!result)
            {
                throw new InvalidOperationException("DrawFromDeck failed");
            }
            
            _gameEventBus.Publish(new PlayerDeckUpdateEvent(
                TurnContext.CurrentTurnPlayer.CardsHandler.MainDeck, 
                TurnContext.CurrentTurnPlayer.Id));
            
            _gameEventBus.Publish(new PlayerHandUpdateEvent(
                TurnContext.CurrentTurnPlayer.CardsHandler.PlayerHand, 
                TurnContext.CurrentTurnPlayer.Id));

            if (CurrentPhase.CurrentStep == GameStep.ProceedToNextPhase)
            {
                HandleAdvancePhaseEffectPriority();
            }
            
            return new CommandResponse(GameStateResult.Success);
        }

        public CommandResponse ClickCardInHand(ICardInstance card)
        {
            // Codar o comando.
            return new CommandResponse(GameStateResult.Success);
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
                if (CurrentPhase.Phase == GamePhase.EndPhase)
                {
                    _gameEventBus.Publish(new PhaseEndEvent(CurrentPhase.Phase));
                    TurnChange();
                    return;
                }
                _gameEventBus.Publish(new PhaseEndEvent(CurrentPhase.Phase));
                _currentPhaseIndex++;
                _gameEventBus.Publish(new PhaseBeginEvent(CurrentPhase.Phase));
                CurrentPhase.Init();
                if (CurrentPhase.CurrentStep != GameStep.ProceedToNextPhase)
                    break;
            }
        }

        private void TurnChange()
        {
            TurnContext.AdvanceTurn();
            _gameEventBus.Publish(new TurnChangeEvent(TurnContext.CurrentTurn));
            _currentPhaseIndex = 0;
            Init();
        }
    }
}