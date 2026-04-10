using System;
using System.Collections.Generic;
using Ygo.Core.Enums;
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
        private GameHandler _gameHandler;

        public GameState(GameHandler gameHandler)
        {
            _gameHandler = gameHandler;
        }
        
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

        public DrawFromDeckResponse TryDrawFromDeck(Guid playerId)
        {
            if (CurrentPhase.Phase != GamePhase.DrawPhase)
                return new DrawFromDeckResponse(GameStateResult.IncorrectPhase);
            if (TurnContext.CurrentTurnPlayer.Id != playerId)
                return new DrawFromDeckResponse(GameStateResult.IncorrectPlayer);
            
            var result = CurrentPhase.DrawFromDeck();
            if (!result)
            {
                throw new InvalidOperationException("DrawFromDeck failed");
            }
            
            if(CurrentPhase.CurrentStep == GameStep.ProceedToNextPhase)
                AdvancePhase();

            return new DrawFromDeckResponse(GameStateResult.Success);
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
            while (true)
            {
                _currentPhaseIndex++;
                CurrentPhase.Init();
                if (CurrentPhase.CurrentStep == GameStep.ProceedToNextPhase)
                {
                    if (CurrentPhase.Phase == GamePhase.EndPhase)
                    {
                        TurnChange();
                        return;
                    }

                    continue;
                }

                break;
            }
        }

        private void TurnChange()
        {
            TurnContext.AdvanceTurn();
            _currentPhaseIndex = 0;
            Init();
        }
    }
}