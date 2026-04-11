using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Ygo.Application;
using Ygo.Controller.Card;
using Ygo.Core;
using Ygo.Core.Abstract;
using Ygo.Core.Commands;
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
        private TurnContext _context;
        private Action<ICardInstance> _onClick;
        private CardsHandler _cardsHandler;
        private CardControllerRegistry _registry;

        public void Init(
            GameCommandBus commandBus, 
            GameEventBus eventBus, 
            TurnContext context,
            CardControllerRegistry registry,
            Action<ICardInstance> onEnter
            )
        {
            foreach (var cardController in cardControllers)
            {
                cardController.Init(onEnter, ClickCard);
            }
            eventBus.Subscribe<CardDrawnEvent>(OnCardDrawn);
            eventBus.Subscribe<PointOfViewUpdateEvent>(OnPointOfViewUpdate);
            _onClick = card =>
            {
                commandBus.Send(new CardInHandClickCommand(PlayerId, card));
            };
            _context = context;
            _registry = registry;
        }

        private void OnPointOfViewUpdate(PointOfViewUpdateEvent e)
        {
            if (pointOfView == PointOfView.Top)
            {
                PlayerId = e.OpponentId;
                SetCardsHandler();
                return;
            }
            PlayerId = e.PointOfViewId;
            SetCardsHandler();
        }

        private void SetCardsHandler()
        {
            var player = _context.Players.FirstOrDefault(x => x.Id == PlayerId);
            if(player == null)
                throw new InvalidOperationException("Player not found");
            _cardsHandler = player.CardsHandler;
        }

        private void ClickCard(ICardInstance card)
        {
            _onClick?.Invoke(card);
        }
        
        private void OnCardDrawn(CardDrawnEvent e)
        {
            if (e.PlayerId != PlayerId)
                return;

            var cards = _cardsHandler.PlayerHand;
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
                _registry.Register(cards[i], cardControllers[i]);
            }

            foreach (var card in cardControllers)
            {
                if(card.Dirty)
                    card.Disable();
            }
        }
    }
}