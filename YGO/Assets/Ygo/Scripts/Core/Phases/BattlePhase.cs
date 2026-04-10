using System;
using System.Collections.Generic;
using System.Linq;
using Ygo.Core.Abstract;
using Ygo.Core.Enums;
using Ygo.Core.Phases.Abstract;
using Ygo.Core.Response;

namespace Ygo.Core.Phases
{
    public class BattlePhase : BaseGamePhase
    {
        public BattlePhase(TurnContext context, Action onGameStepChanged) : base(context, onGameStepChanged)
        {
        }

        public override GamePhase Phase => GamePhase.BasePhase;

        public override void Init()
        {
            if (_context.CurrentTurn <= 1)
            {
                ChangeStep(GameStep.ProceedToNextPhase);
                return;
            }
            ChangeStep(GameStep.Battle);
        }

        public override ClickedOnCardResponse ClickedOnCardInField(ICardInstance card)
        {
            if (CurrentStep != GameStep.Battle)
            {
                return new ClickedOnCardResponse(null);
            }
            
            if (!card.IsValidMonster)
                throw new InvalidOperationException($"Card is not a valid monster");
            
            return new ClickedOnCardResponse(card)
            {
                Attack = card.CanAttack
            };
        }

        public override CheckAttackTargetsResponse CheckAttackTargets(ICardInstance card)
        {
            var attackTargetsList = _context
                .OpponentPlayer
                .BoardHandler
                .MonsterZones
                .Where(x => !x.IsFree)
                .Select(x => x.CardInZone)
                .ToList();
            
            var response = new CheckAttackTargetsResponse(card, attackTargetsList)
            {
                CanAttackDirectly = true
            };
            
            ChangeStep(GameStep.SelectingAttackTarget);
            return response;
        }

        public override BattleResponse DeclareAttack(ICardInstance attacker, ICardInstance target)
        {
            if (attacker == null)
            {
                throw new InvalidOperationException("Attacker is null");
            }
            
            _context.SetBattleContext(attacker, target);
            
            ChangeStep(GameStep.AttackingDeclaration);
            return new BattleResponse(attacker, target);
        }
        
        public override void ContinueTheDamageStep()
        {
            switch (CurrentStep)
            {
                case GameStep.AttackingDeclaration:
                    _context.BattleContext.Attacker.SetAttacked();
                    ChangeStep(GameStep.StartOfDamageStep);
                    break;
                case GameStep.StartOfDamageStep:
                    ChangeStep(GameStep.BeforeDamageCalculation);
                    break;
                case GameStep.BeforeDamageCalculation:
                    BeforeDamageCalculation();
                    ChangeStep(GameStep.DamageCalculation);
                    break;
                case GameStep.DamageCalculation:
                    DamageCalculation();
                    ChangeStep(GameStep.AfterDamageCalculation);
                    break;
                case GameStep.AfterDamageCalculation:
                    ChangeStep(GameStep.EndOfDamageStep);
                    break;
                case GameStep.EndOfDamageStep:
                    EndOfDamageStep();
                    ChangeStep(GameStep.Battle);
                    break;
                default:
                    throw new InvalidOperationException("Invalid GameStep");
            }
        }

        private void BeforeDamageCalculation()
        {
            if (_context.BattleContext.DirectAttack)
                return;
            
            if(_context.BattleContext.Target.IsFaceDown)
                _context.BattleContext.Target.Flip();
        }

        private void DamageCalculation()
        {
            var attacker = _context.BattleContext.Attacker;
            var target = _context.BattleContext.Target;
            var attackerPower = (attacker.IsInDefense ? attacker.CurrentDef : attacker.CurrentAtk).GetValueOrDefault();
            var targetPower = target == null ? 0 : (target.IsInDefense ? target.CurrentDef : target.CurrentAtk).GetValueOrDefault();
            
            var damage = attackerPower - targetPower;

            if (damage < 0)
            {
                MakeAttackerLose(damage);
            }
            else if (damage > 0)
            {
                MakeTargetLose(damage * -1);
            }
            else
            {
                DoTie(attackerPower);
            }
        }

        private void MakeAttackerLose(int damage)
        {
            _context.PointOfViewPlayer.ChangeLifePoints(damage);

            if (_context.BattleContext.DirectAttack)
                return;
            
            if(!_context.BattleContext.Target.IsInDefense)
                _context.BattleContext.Attacker.DestroyByBattle();
        }

        private void MakeTargetLose(int damage)
        {
            if (_context.BattleContext.DirectAttack)
            {
                _context.OpponentPlayer.ChangeLifePoints(damage);
                return;
            }
            
            _context.BattleContext.Target.DestroyByBattle();
            
            if (!_context.BattleContext.Target.IsInDefense)
                _context.OpponentPlayer.ChangeLifePoints(damage);
        }

        private void DoTie(int attacker)
        {
            if (attacker == 0)
                return;

            if (_context.BattleContext.DirectAttack)
                return;

            if (_context.BattleContext.Target.IsInDefense) 
                return;
            
            _context.BattleContext.Attacker.DestroyByBattle();
            _context.BattleContext.Target.DestroyByBattle();
        }

        private void EndOfDamageStep()
        {
            _context.BattleContext.Attacker.ClearDestroyed();
            _context.BattleContext.Target?.ClearDestroyed();
            _context.ClearBattleContext();
        }
        
        public override void GoToNextPhase()
        {
            if (CurrentStep != GameStep.Battle)
                return;
            
            ChangeStep(GameStep.ProceedToNextPhase);
        }
    }
}