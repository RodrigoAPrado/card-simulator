using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Ygo.Controller.Card;
using Ygo.Controller.Field;
using Ygo.Core;
using Ygo.Core.Abstract;
using Ygo.Core.Actions;
using Ygo.Core.Board;
using Ygo.Core.Board.Abstract;
using Ygo.Core.Commands;
using Ygo.Core.Events;
using Ygo.Core.Events.Abstract;
using Ygo.Core.Interaction.Abstract;

namespace Ygo.Controller
{
    public class FieldController : MonoBehaviour
    {
        [field:SerializeField]
        private ZoneController[] frontRowZones;
        [field:SerializeField]
        private CardController[] frontRowCards;
        [field: SerializeField] 
        private PointOfView pointOfView;

        private Guid _requesterId;
        private Guid _ownerId;
        private TurnContext _context;
        private BoardHandler _boardHandler;
        private CardControllerRegistry _registry;
        
        public void Init(
            GameCommandBus commandBus, 
            GameEventBus eventBus, 
            TurnContext context,
            CardControllerRegistry registry,
            Action<ICardInstance, bool> onEnter
            )
        {
            foreach (var cardController in frontRowCards)
            {
                cardController.Init(onEnter, card =>
                {
                    commandBus.Send(new CardOnFieldClickCommand(_requesterId, _ownerId, card));
                });
            }

            foreach (var zoneController in frontRowZones)
            {
                zoneController.Init(zone =>
                {
                    commandBus.Send(new ZoneClickCommand(_requesterId, _ownerId, zone));
                });
            }
            eventBus.Subscribe<PointOfViewUpdateEvent>(OnPointOfViewUpdate);
            eventBus.Subscribe<InteractionStateSetEvent>(OnInteractionStateSet);
            eventBus.Subscribe<AttackDeclarationEvent>(OnAttackDeclaration);
            eventBus.Subscribe<FlipEvent>(OnCardSwitchedPosition);
            eventBus.Subscribe<NormalSummonEvent>(OnNormalSummon);
            eventBus.Subscribe<NormalSetEvent>(OnNormalSummon);
            eventBus.Subscribe<FlipSummonEvent>(OnCardSwitchedPosition);
            eventBus.Subscribe<SwitchMonsterToAttackEvent>(OnCardSwitchedPosition);
            eventBus.Subscribe<SwitchMonsterToDefenseEvent>(OnCardSwitchedPosition);
            eventBus.Subscribe<CardSentToGraveByDestructionEvent>(OnCardSentToGraveByDestruction);
            eventBus.Subscribe<MonsterTributedEvent>(OnMonsterTributed);
            _context = context;
            _registry = registry;
        }
        
        private void OnPointOfViewUpdate(PointOfViewUpdateEvent e)
        {
            _requesterId = e.PointOfViewId;
            _ownerId = pointOfView == PointOfView.Top ? e.OpponentId : e.PointOfViewId;
            SetBoardHandler();
            UpdateBoard();
        }

        private void SetBoardHandler()
        {
            var player = _context.Players.FirstOrDefault(x => x.Id == _ownerId);
            if(player == null)
                throw new InvalidOperationException("Player not found");
            _boardHandler = player.BoardHandler;
        }

        private void OnInteractionStateSet(InteractionStateSetEvent e)
        {
            if (e.RequesterId != _ownerId)
                return;
            
            if (e.InteractionState is ZoneSelectionState zoneState)
            {
                foreach (var controller in frontRowZones)
                {
                    controller.ToggleHighlight(false);
                }
                foreach (var zone in zoneState.AvailableZones)
                {
                    var zoneController = frontRowZones.FirstOrDefault(x => x.Zone == zone);
                    zoneController?.ToggleHighlight(true);
                }
                return;
            }

            if (e.InteractionState is MonsterCardSelectionState monsterState)
            {
                foreach (var controller in frontRowCards)
                {
                    controller.ToggleHighlight(false);
                }
                foreach (var card in monsterState.AvailableCards)
                {
                    var cardController = frontRowCards.FirstOrDefault(x => x.Card == card);
                    cardController?.ToggleHighlight(true);
                }
            }
        }

        private void OnAttackDeclaration(AttackDeclarationEvent e)
        {
            foreach (var cardController in frontRowCards)
            {
                cardController.ToggleHighlight(false);
            }
        }

        private void OnNormalSummon(NormalSummonEvent e)
        {
            if (e.PlayerId != _ownerId)
                return;
            foreach (var zone in frontRowZones)
            {
                zone.ToggleHighlight(false);
            }
            UpdateBoard();
        }

        private void OnCardSwitchedPosition(IGameEvent e)
        {
            UpdateBoard();
        }

        private void UpdateBoard()
        {
            foreach (var card in frontRowCards)
            {
                card.SetDirty();
                card.ToggleHighlight(false);
            }
            
            foreach (var zone in frontRowZones)
            {
                zone.SetBoardZone(_boardHandler.MonsterZones[(int)zone.Position-2]);
                if (zone.Zone.IsFree)
                    continue;
                var card = frontRowCards[(int)zone.Position-2];
                card?.Enable();
                card?.UpdateCard(zone.Zone.CardInZone);
                if (card != null)
                {
                    _registry.Register(zone.Zone.CardInZone, card);
                }
            }
            
            foreach (var card in frontRowCards.Where(card => card.Dirty))
            {
                card.Disable();
            }
        }

        private void OnCardSentToGraveByDestruction(CardSentToGraveByDestructionEvent e)
        {
            if (e.Zone == null)
                return;
            var zone = frontRowZones.FirstOrDefault(x => x.Zone == e.Zone);
            if (zone == null)
                return;
            var card = frontRowCards[(int)zone.Position-2];
            card.Disable();
        }

        private void OnMonsterTributed(MonsterTributedEvent e)
        {
            if (e.Zone == null)
                return;
            var zone = frontRowZones.FirstOrDefault(x => x.Zone == e.Zone);
            if (zone == null)
                return;
            var card = frontRowCards[(int)zone.Position-2];
            card.Disable();
        }
    }
}