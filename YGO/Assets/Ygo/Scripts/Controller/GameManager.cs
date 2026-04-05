using System.Collections.Generic;
using UnityEngine;
using Ygo.Application;
using Ygo.Controller.Card;
using Ygo.Controller.Field;
using Ygo.Core.Abstract;
using Ygo.Controller.Hand;
using Ygo.Service;
using Ygo.View;

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
        [field: SerializeField]
        private HandController handController;
        
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
        private TextViewUI PhaseText;
        
        public void Awake()
        {
            var service = new CardLoaderService();
            var data = service.LoadCards();
            _application = new GameApplication(data);
            _application.InitializeGame();
            zoomCard.SetZoomMode();
            _playerHand = new List<CardController>();
            UpdatePlayerHand();
            mainDeckController.SetDeckSize(_application.PointOfViewPlayer.CardsHandler.MainDeck.Count);
            mainDeckController.SubscribeToMainDeckClicked(DrawFromDeck);
            _application.SubscribeToPhaseChange(OnPhaseChange);
            PhaseText.SetText(_application.CurrentPhase.Name);
            handController.Init(OnNormalSummon, OnSet, OnTributeSummon, OnTributeSet);
            handController.HideAll();
        }

        private void UpdatePlayerHand()
        {
            foreach (var card in _playerHand)
            {
                card.SetDirty();
            }
            
            for (var i = 0; i < _application.PointOfViewPlayer.CardsHandler.PlayerHand.Count; i++)
            {
                if (_playerHand.Count <= i)
                {
                    InstantiateCardController(_application.PointOfViewPlayer.CardsHandler.PlayerHand[i]);
                    continue;
                }
                _playerHand[i].Enable();
                _playerHand[i].UpdateCard(_application.PointOfViewPlayer.CardsHandler.PlayerHand[i]);
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
            mainDeckController.SetDeckSize(_application.PointOfViewPlayer.CardsHandler.MainDeck.Count);
        }

        private void ClickedOnCardInHand(CardController cardController)
        {
            var response = _application.CurrentPhase.ClickedOnCardInHand(cardController.CardInstance);
            handController.Show(response, cardController.transform.position.x);
        }

        private void OnPhaseChange()
        {
            PhaseText.SetText(_application.CurrentPhase.Name);
        }

        private void OnNormalSummon()
        {
            
        }

        private void OnSet()
        {
            
        }

        private void OnTributeSummon()
        {
            
        }

        private void OnTributeSet()
        {
            
        }
        
    }
}