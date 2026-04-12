using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Ygo.Controller.Card;
using Ygo.Controller.Field;
using Ygo.Core;
using Ygo.Core.Abstract;
using Ygo.Core.Board;
using Ygo.Core.Board.Abstract;
using Ygo.Core.Commands;
using Ygo.Core.Events;
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
            Action<ICardInstance> onEnter
            )
        {
            foreach (var cardController in frontRowCards)
            {
                cardController.Init(onEnter, ClickCard);
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
            eventBus.Subscribe<NormalSummonEvent>(OnNormalSummon);
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
        
        private void ClickCard(ICardInstance card)
        {
            Debug.Log("clicked card on field");
        }

        private void OnInteractionStateSet(InteractionStateSetEvent e)
        {
            if (e.PlayerId != _ownerId)
                return;
            if (e.InteractionState is not ZoneSelectionState state)
                return;
            foreach (var zone in state.AvailableZones)
            {
                var zoneController = frontRowZones.FirstOrDefault(x => x.Zone == zone);
                zoneController?.ToggleHighlight(true);
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

        private void UpdateBoard()
        {
            foreach (var card in frontRowCards)
            {
                card.SetDirty();
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
    }
}