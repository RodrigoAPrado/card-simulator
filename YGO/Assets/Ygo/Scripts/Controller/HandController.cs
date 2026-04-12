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

        private Guid _requesterId;
        private Guid _ownerId;
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
            eventBus.Subscribe<NormalSummonEvent>(OnNormalSummon);
            _onClick = card =>
            {
                commandBus.Send(new CardInHandClickCommand(_requesterId, _ownerId, card));
            };
            _context = context;
            _registry = registry;
            _requesterId = _context.PointOfViewPlayer.Id;
        }

        private void OnPointOfViewUpdate(PointOfViewUpdateEvent e)
        {
            _requesterId = e.PointOfViewId;
            _ownerId = pointOfView == PointOfView.Top ? e.OpponentId : e.PointOfViewId;
            SetCardsHandler();
            UpdateHand();
        }

        private void SetCardsHandler()
        {
            var player = _context.Players.FirstOrDefault(x => x.Id == _ownerId);
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
            if (e.PlayerId != _ownerId)
                return;
            UpdateHand();
        }
        
        private void OnNormalSummon(NormalSummonEvent e)
        {
            if (e.PlayerId != _ownerId)
                return;
            UpdateHand();
        }

        private void UpdateHand()
        {
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
                cardControllers[i].UpdateCard(cards[i], pointOfView == PointOfView.Top);
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