using System;
using System.Collections.Generic;
using Ygo.Core.Enums;
using Ygo.Core.Phases;
using Ygo.Core.Phases.Abstract;

namespace Ygo.Core
{
    public class GameState
    {
        public TurnContext TurnContext { get; private set; }
        public IGamePhase CurrentPhase => _phases[_currentPhaseIndex];
        private List<IGamePhase> _phases;
        private int _currentPhaseIndex;
        
        public event Action OnPhaseChange;
        public event Action OnTurnChange;
        public event Action OnBattleUpdate;
        
        public void Setup(TurnContext turnContext)
        {
            TurnContext = turnContext;
            _phases = new List<IGamePhase>
            {
                new DrawPhase(TurnContext, OnGameStepChanged),
                new StandbyPhase(TurnContext, OnGameStepChanged),
                new MainPhase1(TurnContext, OnGameStepChanged),
                new BattlePhase(TurnContext, OnGameStepChanged),
                new MainPhase2(TurnContext, OnGameStepChanged),
                new EndPhase(TurnContext, OnGameStepChanged)
            };
            _currentPhaseIndex = 0;
        }

        public void Init()
        {
            CurrentPhase.Init();
        }

        public void SubscribeToPhaseChange(Action action)
        {
            OnPhaseChange += action;
        }

        public void UnsubscribeToPhaseChange(Action action)
        {
            OnPhaseChange -= action;
        }

        public void SubscribeToTurnChange(Action action)
        {
            OnTurnChange += action;
        }

        public void UnsubscribeToTurnChange(Action action)
        {
            OnTurnChange -= action;
        }

        public void SubscribeToBattleUpdate(Action action)
        {
            OnBattleUpdate += action;
        }

        public void UnsubscribeToBattleUpdate(Action action)
        {
            OnBattleUpdate -= action;
        }

        private void OnGameStepChanged()
        {
            switch (CurrentPhase.CurrentStep)
            {
                case GameStep.ProceedToNextPhase when CurrentPhase is EndPhase:
                    TurnChange();
                    return;
                case GameStep.AttackingDeclaration when CurrentPhase is BattlePhase:
                    CurrentPhase.ContinueTheDamageStep();
                    return;
                case GameStep.StartOfDamageStep when CurrentPhase is BattlePhase:
                    CurrentPhase.ContinueTheDamageStep();
                    return;
                case GameStep.BeforeDamageCalculation when CurrentPhase is BattlePhase:
                    CurrentPhase.ContinueTheDamageStep();
                    return;
                case GameStep.DamageCalculation when CurrentPhase is BattlePhase:
                    CurrentPhase.ContinueTheDamageStep();
                    return;
                case GameStep.AfterDamageCalculation when CurrentPhase is BattlePhase:
                    CurrentPhase.ContinueTheDamageStep();
                    return;
                case GameStep.EndOfDamageStep when CurrentPhase is BattlePhase:
                    CheckSendToGraveyard();
                    OnBattleUpdate?.Invoke();
                    CurrentPhase.ContinueTheDamageStep();
                    return;
                case GameStep.ProceedToNextPhase:
                    AdvancePhase();
                    return;
                case GameStep.OnMonsterSummoned when CurrentPhase is MainPhase1 or MainPhase2:
                    CurrentPhase.ToOpenGameStep();
                    break;
            }
        }

        private void CheckSendToGraveyard()
        {
            if (TurnContext.BattleContext.DirectAttack)
                return;
            if (TurnContext.BattleContext.Target.IsDestroyed)
            {
                var response = TurnContext.BattleContext.Target.Zone.TryRemoveCard();
                if (response.Fail || response.CardRemoved != TurnContext.BattleContext.Target)
                    throw new InvalidOperationException("Target is not in correct zone.");
                TurnContext.BattleContext.Target.SendToGraveyard();
            }

            if (TurnContext.BattleContext.Attacker.IsDestroyed)
            {
                var response = TurnContext.BattleContext.Attacker.Zone.TryRemoveCard();
                if (response.Fail || response.CardRemoved != TurnContext.BattleContext.Attacker)
                    throw new InvalidOperationException("Attacker is not in correct zone.");
                TurnContext.BattleContext.Attacker.SendToGraveyard();
            }
        }

        private void AdvancePhase()
        {
            _currentPhaseIndex++;
            CurrentPhase.Init();
            OnPhaseChange?.Invoke();
        }

        private void TurnChange()
        {
            TurnContext.AdvanceTurn();
            _currentPhaseIndex = 0;
            OnTurnChange?.Invoke();
            Init();
        }
    }
}