using System;
using Ygo.Core.Abstract;
using Ygo.Core.Enums;
using Ygo.Core.Events.Abstract;

namespace Ygo.Core
{
    public class BattleState : IBattleState
    {
        public ICardInstance Attacker { get; }
        public ICardInstance Defender { get; }
        public BattleStep CurrentStep { get; private set; }
        private readonly GameState _gameState;
        private readonly Guid _attackerPlayer;
        private readonly Guid _defenderPlayer;
        public BattleState(GameState gameState, Guid attackerPlayer, Guid defenderPlayer, ICardInstance attacker, ICardInstance defender)
        {
            Attacker = attacker;
            Defender = defender;
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
            if(Defender.IsFaceDown)
                _gameState.DoFlip(_defenderPlayer, Defender);
            // Effect Trigger, passive effects
        }

        private void DamageCalculation()
        {
            var attackerValue = Attacker.IsInDefense ? Attacker.Data.MonsterData.Def : Attacker.Data.MonsterData.Atk;
            var defenderValue = Defender.IsInDefense ? Defender.Data.MonsterData.Def : Defender.Data.MonsterData.Atk;

            var damage = attackerValue - defenderValue;

            switch (damage)
            {
                case 0:
                    OnZeroDamage(attackerValue > 0);
                    break;
                case < 0:
                    OnAttackerLoses(damage*-1);
                    break;
                case >0:
                    OnDefenderLoses(damage);
                    break;
            }
        }

        private void OnZeroDamage(bool attackerValueAboveZero)
        {
            if (!attackerValueAboveZero) 
                return;
            if (Defender.IsInDefense) 
                return;
            Attacker.DestroyByBattle();
            Defender.DestroyByBattle();
        }

        private void OnAttackerLoses(int damage)
        {
            _gameState.DealBattleDamage(_attackerPlayer, damage);
            if (!Defender.IsInDefense)
                _gameState.DestroyMonsterByBattle(_attackerPlayer, Attacker);
        }

        private void OnDefenderLoses(int damage)
        {
            if (!Defender.IsInDefense)
                _gameState.DealBattleDamage(_defenderPlayer, damage);
            _gameState.DestroyMonsterByBattle(_defenderPlayer, Defender);
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