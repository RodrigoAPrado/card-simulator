using System;
using Ygo.Core.Abstract;
using Ygo.Core.Enums;
using Ygo.Core.Events.Abstract;

namespace Ygo.Core
{
    public class DirectBattleState : IBattleState
    {
        public ICardInstance Attacker { get; }
        public ICardInstance Defender => null;
        public BattleStep CurrentStep { get; private set; }
        private readonly GameState _gameState;
        private readonly Guid _attackerPlayer;
        private readonly Guid _defenderPlayer;
        
        public DirectBattleState(GameState gameState, Guid attackerPlayer, Guid defenderPlayer, ICardInstance attacker)
        {
            Attacker = attacker;
            CurrentStep = BattleStep.AttackDeclaration;
            _gameState = gameState;
            _attackerPlayer = attackerPlayer;
            _defenderPlayer = defenderPlayer;
        }
        
        public void ProceedBattleState()
        {
            switch (CurrentStep)
            {
                case BattleStep.StartOfDamageStep:
                    StartOfDamageStep();
                    break;
                case BattleStep.BeforeDamageCalculation:
                    BeforeDamageCalculation();
                    break;
                case BattleStep.DamageCalculation:
                    DamageCalculation();
                    break;
                case BattleStep.AfterDamageCalculation:
                    AfterDamageCalculation();
                    break;
                case BattleStep.EndOfDamageStep:
                    EndOfDamageStep();
                    break;
            }
            CurrentStep++;
            _gameState.BattleStateChanged();
        }

        private void StartOfDamageStep()
        {
            // Effect Trigger
            Attacker.SetAttacked();
        }

        private void BeforeDamageCalculation()
        {
            // Effect Trigger, passive effects
        }

        private void DamageCalculation()
        {
            var attackerValue = Attacker.IsInDefense ? Attacker.Data.MonsterData.Def : Attacker.Data.MonsterData.Atk;

            var damage = attackerValue;

            switch (damage)
            {
                case < 0:
                    OnAttackerLoses(damage*-1);
                    break;
                case >0:
                    OnDefenderLoses(damage);
                    break;
            }
        }

        private void OnAttackerLoses(int damage)
        {
            _gameState.DealBattleDamage(_attackerPlayer, damage);
        }

        private void OnDefenderLoses(int damage)
        {
            _gameState.DealBattleDamage(_defenderPlayer, damage);
        }

        private void AfterDamageCalculation()
        {
            // Effect Trigger FLIP/AfterDmg
        }

        private void EndOfDamageStep()
        {
            _gameState.SendDestroyedCardToGrave();
            // Effect Trigger, Send to GY effect, destroyed, etc
            _gameState.ClearDestroyedCards();
        }
    }
}