using System;
using System.Collections.Generic;
using UnityEngine;
using Ygo.Application;
using Ygo.Controller.Card;
using Ygo.Core;
using Ygo.Core.Abstract;

namespace Ygo.Controller
{
    public class HandController : MonoBehaviour
    {
        [field:SerializeField]
        private CardController[] cardControllers;
        [field:SerializeField]
        private bool PoVPlayer { get; set; }

        public void Init(GameCommandBus commandBus, GameEventBus gameEventBus, Action<ICardInstance> onEnter)
        {
            foreach (var cardController in cardControllers)
            {
                cardController.Init(onEnter, ClickCard);
            }
        }

        private void ClickCard(ICardInstance card)
        {
            Debug.Log("clicked card on hand");
        }
        
        private void UpdatePlayerHand(IList<ICardInstance> cards)
        {
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