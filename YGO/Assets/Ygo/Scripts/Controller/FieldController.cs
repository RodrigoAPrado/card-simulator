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
using Ygo.Core.Events;

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
        private Guid PlayerId { get; set; }
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
                zoneController.Init(ClickZone);
            }
            eventBus.Subscribe<PointOfViewUpdateEvent>(OnPointOfViewUpdate);
            eventBus.Subscribe<PlayerFieldUpdateEvent>(OnUpdate);
            _context = context;
            _registry = registry;
        }
        
        private void OnPointOfViewUpdate(PointOfViewUpdateEvent e)
        {
            if (pointOfView == PointOfView.Top)
            {
                PlayerId = e.OpponentId;
                SetBoardHandler();
                return;
            }
            PlayerId = e.PointOfViewId;
            SetBoardHandler();
        }

        private void SetBoardHandler()
        {
            var player = _context.Players.FirstOrDefault(x => x.Id == PlayerId);
            if(player == null)
                throw new InvalidOperationException("Player not found");
            _boardHandler = player.BoardHandler;
        }

        private void ClickZone(IBoardZone zone)
        {
            
            Debug.Log("clicked zone");
        }
        
        private void ClickCard(ICardInstance card)
        {
            Debug.Log("clicked card on field");
        }

        private void OnUpdate(PlayerFieldUpdateEvent e)
        {
            if (e.PlayerId != PlayerId)
                return;
            
            foreach (var card in frontRowCards)
            {
                card.SetDirty();
            }
            
            foreach (var zone in frontRowZones)
            {
                zone.SetBoardZone(_boardHandler.MonsterZones.FirstOrDefault(x => x.Position == zone.Position));
                if (zone.Zone.IsFree)
                    continue;
                var card = frontRowCards.FirstOrDefault(x => x.ZonePosition == zone.Position);
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