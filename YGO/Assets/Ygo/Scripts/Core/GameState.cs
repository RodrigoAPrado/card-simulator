using System;
using System.Collections.Generic;
using System.Linq;
using Ygo.Core.Abstract;
using Ygo.Core.Actions.Abstract;
using Ygo.Core.Board.Abstract;
using Ygo.Core.Enums;
using Ygo.Core.Events;
using Ygo.Core.Events.Abstract;
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
        private readonly GameHandler _gameHandler;
        private IBattleState _battleState;

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

        public CommandResponse ClickedOnMainDeck(Guid requesterId, Guid ownerId)
        {
            return ProcessActionQuery(CurrentPhase.ClickedOnMainDeck(requesterId, ownerId));
        }

        public CommandResponse ClickCardInHand(Guid requesterId, Guid playerId, ICardInstance card)
        {
            return ProcessActionQuery(CurrentPhase.ClickedOnCardInHand(requesterId, playerId, card));
        }

        public CommandResponse ClickCardOnField(Guid requesterId, Guid playerId, ICardInstance card)
        {
            return ProcessActionQuery(CurrentPhase.ClickedOnCardOnField(requesterId, playerId, card));
        }

        public CommandResponse ClickZone(Guid requesterId, Guid playerId, IBoardZone zone)
        {
            return ProcessActionQuery(CurrentPhase.ClickedOnZone(requesterId, playerId, zone));
        }
        
        public CommandResponse ClickNextPhase(Guid requesterId)
        {
            return ProcessActionQuery(CurrentPhase.ClickedOnNextPhase(requesterId));
        }

        public void InitGame()
        {
            StartTurn();
        }

        public void DrawFromDeck(Guid ownerId)
        {
            var result = CurrentPhase.DrawForTurn(ownerId);
            if (result.ActionState != ActionState.Success)
            {
                throw new InvalidOperationException("Invalid action!");
            }
            
            _gameEventBus.Publish(new CardDrawnEvent(ownerId));
        }

        public void CheckNormalSummon(Guid ownerId, ICardInstance card)
        {
            CurrentPhase.CheckNormalSummon(ownerId, card);
        }
        
        public void CheckNormalSet(Guid ownerId, ICardInstance card)
        {
            CurrentPhase.CheckNormalSet(ownerId, card);
        }

        public void DoNormalSummon(Guid ownerId, ICardInstance card, IBoardZone boardZone)
        {
            CurrentPhase.DoNormalSummon(ownerId, card, boardZone);
            _gameEventBus.Publish(new NormalSummonEvent(ownerId, card, boardZone));
        }

        public void DoNormalSet(Guid ownerId, ICardInstance card, IBoardZone boardZone)
        {
            CurrentPhase.DoNormalSet(ownerId, card, boardZone);
            _gameEventBus.Publish(new NormalSetEvent(ownerId, card, boardZone));
        }

        public void DoFlipSummon(Guid ownerId, ICardInstance card)
        {
            CurrentPhase.DoFlipSummon(ownerId, card);
            _gameEventBus.Publish(new FlipSummonEvent(ownerId, card));
        }

        public void DoFlip(Guid ownerId, ICardInstance card)
        {
            CurrentPhase.DoTryFlip(ownerId, card);
            _gameEventBus.Publish(new FlipEvent(ownerId, card));
        }
        
        public void DoSwitchMonsterToAttack(Guid ownerId, ICardInstance card)
        {
            CurrentPhase.DoSwitchMonsterToAttack(ownerId, card);
            _gameEventBus.Publish(new SwitchMonsterToAttackEvent(ownerId, card));
        }
        
        public void DoSwitchMonsterToDefense(Guid ownerId, ICardInstance card)
        {
            CurrentPhase.DoSwitchMonsterToDefense(ownerId, card);
            _gameEventBus.Publish(new SwitchMonsterToDefenseEvent(ownerId, card));
        }

        public void CheckAttack(Guid ownerId, ICardInstance card)
        {
            CurrentPhase.CheckAttack(ownerId, card);
        }

        public void DeclareAttack(Guid ownerId, ICardInstance attacker, ICardInstance defender)
        {
            CurrentPhase.DeclareAttack(ownerId, attacker, defender);
            _gameEventBus.Publish(new AttackDeclarationEvent(ownerId, attacker, defender));
        }

        public void DeclareDirectAttack(Guid ownerId, ICardInstance attacker)
        {
            CurrentPhase.DeclareDirectAttack(ownerId, attacker);
            _gameEventBus.Publish(new DirectAttackDeclarationEvent(ownerId, attacker));
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

        public void BattleStateChanged()
        {
            _gameEventBus.Publish(new BattleStateProgressEvent(_battleState.Attacker, _battleState.Defender, _battleState.CurrentStep));
            ResolveGameStep();
        }

        public void DealBattleDamage(Guid playerId, int damage)
        {
            var player = TurnContext.Players.FirstOrDefault(x => x.Id == playerId);
            player?.ChangeLifePoints(-damage);
            //remover o info update futuramente:
            _gameEventBus.Publish(new PlayerInfoUpdateEvent(
                TurnContext.PointOfViewPlayer.PlayerName, 
                TurnContext.PointOfViewPlayer.CurrentLifePoints,
                TurnContext.OpponentPlayer.PlayerName,
                TurnContext.OpponentPlayer.CurrentLifePoints));
            _gameEventBus.Publish(new PlayerTakenBattleDamageEvent(playerId, damage));
        }

        public void DestroyMonsterByBattle(Guid playerId, ICardInstance card)
        {
            card.DestroyByBattle();
            _gameEventBus.Publish(new CardDestroyedByBattleEvent(playerId, card));
        }

        public void SendDestroyedCardToGrave()
        {
            var destroyedCards = new List<ICardInstance>();
            foreach (var p in TurnContext.Players)
            {
                destroyedCards.AddRange(p.CardsHandler.PlayerCards.Where(x => x.IsDestroyed));
            }
            foreach (var dc in destroyedCards)
            {
                IBoardZone boardZone = null;
                if (dc.Zone != null)
                {
                    boardZone = dc.Zone;
                    boardZone.TryRemoveCard();
                }
                dc.SendToGraveyard();
                _gameEventBus.Publish(new CardSentToGraveEvent(dc.OwnerId, boardZone, dc));
            }
        }
        
        public void ClearDestroyedCards()
        {
            var destroyedCards = new List<ICardInstance>();
            foreach (var p in TurnContext.Players)
            {
                destroyedCards.AddRange(p.CardsHandler.PlayerCards.Where(x => x.IsDestroyed));
            }
            foreach (var dc in destroyedCards)
            {
                dc.ClearDestroyed();
            }
        }
        
        private void ResolveGameStep()
        {
            HandleEffectPriority();
            HandlePhaseProgression();
        }

        public void SetBattleState(IBattleState battleState)
        {
            if (_battleState != null)
                throw new InvalidOperationException("BattleState should be null at this point.");
            _battleState = battleState;
        }
        public void ClearBattleState()
        {
            if (_battleState == null)
                throw new InvalidOperationException("BattleState should not be null at this point.");
            _battleState = null;
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
            if(CurrentPhase.CurrentStep == PhaseStep.ProceedToNextPhase)
                AdvancePhase();
            if (CurrentPhase.CurrentStep == PhaseStep.Battle)
                HandleBattleProgression();
        }

        private void HandleBattleProgression()
        {
            if (_battleState.CurrentStep != BattleStep.ReturnToBattlePhase)
            {
                _battleState.ProceedBattleState();
                return;
            }

            var result = CurrentPhase.FinishBattleState();
            if (result.ActionState != ActionState.Success)
                throw new InvalidOperationException($"Not in battle phase {nameof(CurrentPhase)}.");
            
        }
        
        private void HandleEffectPriority()
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
                if (CurrentPhase.CurrentStep != PhaseStep.ProceedToNextPhase)
                    break;
            }
        }

        private void TurnChange()
        {
            TurnContext.AdvanceTurn();
            _gameEventBus.Publish(new PointOfViewUpdateEvent(TurnContext.PointOfViewPlayer.Id, TurnContext.OpponentPlayer.Id));
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