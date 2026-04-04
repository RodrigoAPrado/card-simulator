using System.Collections.Generic;
using UnityEngine;
using Ygo.Application;
using Ygo.Controller.Card;
using Ygo.Controller.Field;
using Ygo.Core.Abstract;
using Ygo.Scripts.View;
using Ygo.Service;

namespace Ygo.Scripts.Controller
{
    public class GameManager : MonoBehaviour
    {
        [Header("CardPrefab")] 
        [field: SerializeField]
        private CardController cardPrefab;

        [Header("HandsArea")] 
        [field: SerializeField]
        private GameObject playerHandArea;
        
        [Header("ZoomCard")]
        [field: SerializeField]
        private CardController zoomCard;

        private GameApplication _application;
        private List<CardController> _playerHand;
 
        [Header("Field")]
        [field: SerializeField]
        private MainDeckController mainDeckController;

        [Header("Phase")] 
        [field: SerializeField]
        private TextView PhaseText;
        
        public void Awake()
        {
            var service = new CardLoaderService();
            var data = service.LoadCards();
            _application = new GameApplication(data);
            _application.InitializeGame();
            zoomCard.SetZoomMode();
            _application.DrawInitialHand();
            _playerHand = new List<CardController>();
            UpdatePlayerHand();
            mainDeckController.SetDeckSize(_application.Deck.Count);
            mainDeckController.SubscribeToMainDeckClicked(DrawFromDeck);
            _application.SubscribeToPhaseChange(OnPhaseChange);
            PhaseText.SetText(_application.CurrentPhase.Name);
        }

        public void ShuffleDeck()
        {
            _application.ShuffleDeck();
        }

        public void DrawCard()
        {
            _application.DrawCard();
            UpdatePlayerHand();
        }

        private void UpdatePlayerHand()
        {
            foreach (var card in _playerHand)
            {
                card.SetDirty();
            }
            
            for (var i = 0; i < _application.PlayerHand.Count; i++)
            {
                if (_playerHand.Count <= i)
                {
                    InstantiateCardController(_application.PlayerHand[i]);
                    continue;
                }
                _playerHand[i].Enable();
                _playerHand[i].UpdateCard(_application.PlayerHand[i]);
            }

            foreach (var card in _playerHand)
            {
                if(card.Dirty)
                    card.Disable();
            }
        }

        private void InstantiateCardController(ICardInstance card)
        {
            var o = Instantiate(cardPrefab, playerHandArea.transform);
            o.SetHandMode();
            o.Init(card, UpdateZoomCard, ClickedOnCardInHand);
            _playerHand.Add(o);
        }

        private void UpdateZoomCard(ICardInstance card)
        {
            zoomCard.Init(card);
        }

        private void DrawFromDeck()
        {
            var drawn = _application.DrawFromDeck();
            
            if (!drawn) return;
            
            UpdatePlayerHand();
            mainDeckController.SetDeckSize(_application.Deck.Count);
        }

        private void ClickedOnCardInHand(ICardInstance cardInstance)
        {
            _application.CurrentPhase.ClickedOnCardInHand(cardInstance);
        }

        private void OnPhaseChange()
        {
            PhaseText.SetText(_application.CurrentPhase.Name);
        }
    }
}