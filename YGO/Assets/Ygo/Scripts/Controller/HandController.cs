using System;
using System.Collections.Generic;
using UnityEngine;
using Ygo.Application;
using Ygo.Controller.Card;
using Ygo.Core;
using Ygo.Core.Abstract;
using Ygo.Core.Events;

namespace Ygo.Controller
{
    public class HandController : MonoBehaviour
    {
        [field:SerializeField]
        private CardController[] cardControllers;
        [field: SerializeField] 
        private PointOfView pointOfView;
        private Guid PlayerId { get; set; }

        public void Init(GameCommandBus commandBus, GameEventBus eventBus, Action<ICardInstance> onEnter)
        {
            foreach (var cardController in cardControllers)
            {
                cardController.Init(onEnter, ClickCard);
            }
            eventBus.Subscribe<PointOfViewUpdateEvent>(OnPointOfViewUpdate);
            eventBus.Subscribe<PlayerHandUpdateEvent>(OnUpdate);
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

        private void ClickCard(ICardInstance card)
        {
            Debug.Log("clicked card on hand");
        }
        
        private void OnUpdate(PlayerHandUpdateEvent e)
        {
            if (e.PlayerId != PlayerId)
                return;
            
            var cards = e.Hand;
            foreach (var card in cardControllers)
            {
                card.SetDirty();
            }
            
            for (var i = 0; i < cards.Count; i++)
            {
                if (cardControllers.Length <= i)
                {
                    continue;
                }
                
                cardControllers[i].Enable();
                cardControllers[i].UpdateCard(cards[i]);
            }

            foreach (var card in cardControllers)
            {
                if(card.Dirty)
                    card.Disable();
            }
        }
    }
}