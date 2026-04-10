using System;
using System.Collections.Generic;
using UnityEngine;
using Ygo.Controller.Card;
using Ygo.Controller.Field;
using Ygo.Core;
using Ygo.Core.Abstract;
using Ygo.Core.Board.Abstract;

namespace Ygo.Controller
{
    public class FieldController : MonoBehaviour
    {
        [field:SerializeField]
        private ZoneController[] frontRowZones;
        [field:SerializeField]
        private CardController[] frontRowCards;
        [field: SerializeField] 
        private bool PoVPlayer;

        public void Init(GameCommandBus commandBus, GameEventBus gameEventBus, Action<ICardInstance> onEnter)
        {
            foreach (var cardController in frontRowCards)
            {
                cardController.Init(onEnter, ClickCard);
            }

            foreach (var zoneController in frontRowZones)
            {
                zoneController.Init(ClickZone);
            }
        }

        private void ClickZone(IBoardZone zone)
        {
            
            Debug.Log("clicked zone");
        }
        
        private void ClickCard(ICardInstance card)
        {
            Debug.Log("clicked card on field");
        }
    }
}