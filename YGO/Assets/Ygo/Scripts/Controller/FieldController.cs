using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Ygo.Controller.Card;
using Ygo.Controller.Field;
using Ygo.Core;
using Ygo.Core.Abstract;
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
        
        public void Init(GameCommandBus commandBus, GameEventBus eventBus, Action<ICardInstance> onEnter)
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
        }

        private void OnPointOfViewUpdate(PointOfViewUpdateEvent e)
        {
            if (pointOfView == PointOfView.Top)
            {
                PlayerId = e.OpponentId;
                return;
            }
            PlayerId = e.PointOfViewId;
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
                zone.SetBoardZone(e.Board.FirstOrDefault(x => x.Position == zone.Position));
                if (zone.Zone.IsFree)
                    continue;
                var card = frontRowCards.FirstOrDefault(x => x.ZonePosition == zone.Position);
                card?.Enable();
                card?.UpdateCard(zone.Zone.CardInZone);
            }
            
            foreach (var card in frontRowCards.Where(card => card.Dirty))
            {
                card.Disable();
            }
        }
    }
}