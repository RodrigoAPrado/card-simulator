using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Ygo.Application;
using Ygo.Controller.Card;
using Ygo.Controller.Field;
using Ygo.Core.Abstract;
using Ygo.Controller.Hand;
using Ygo.Core.Board.Abstract;
using Ygo.Core.Enums;
using Ygo.Service;
using Ygo.View;

namespace Ygo.Scripts.Controller
{
    public class GameManager : MonoBehaviour
    {
        [Header("CardPrefab")] 
        [field: SerializeField]
        private CardController cardPrefab;
        [field: SerializeField]
        private FieldCardController fieldCardPrefab;

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
        [field: SerializeField]
        private List<ZoneController> monsterZoneControllers;
        [field: SerializeField]
        private Transform monsterCardsParent;
        private Dictionary<ZonePosition, FieldCardController> zoneCardControllers;
        

        [Header("Phase")] 
        [field: SerializeField]
        private TextViewUI PhaseText;
        
        private CardController _currentSelectedCardInHand;
        
        public void Awake()
        {
            var service = new CardLoaderService();
            var data = service.LoadCards();
            _application = new GameApplication(data);
            _application.InitializeGame();
            zoomCard.SetZoomMode();
            _playerHand = new List<CardController>();
            UpdatePlayerHand();
            mainDeckController.Init(DrawFromDeck);
            mainDeckController.SetDeckSize(_application.PointOfViewPlayer.CardsHandler.MainDeck.Count);
            _application.SubscribeToPhaseChange(OnPhaseChange);
            PhaseText.SetText(_application.CurrentPhase.Name);
            handController.Init(OnTryNormalSummon, OnTrySet, OnTryTributeSummon, OnTryTributeSet);
            handController.HideAll();
            zoneCardControllers = new Dictionary<ZonePosition, FieldCardController>();
            foreach (var zoneController in monsterZoneControllers)
            {
                var boardZone =  _application.PointOfViewPlayer.BoardHandler.MonsterZones.FirstOrDefault(x =>
                    x.Position == zoneController.Position);
                zoneController.Init(boardZone, OnClickMonsterZone);
            }
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
            _currentSelectedCardInHand = cardController;
        }

        private void OnPhaseChange()
        {
            PhaseText.SetText(_application.CurrentPhase.Name);
        }

        private void OnTryNormalSummon()
        {
            var response = _application.CurrentPhase.CheckWhereToSummonMonster(_currentSelectedCardInHand.CardInstance);
            if (response.CanSummon)
            {
                handController.HideAll();
                foreach (var zoneController in monsterZoneControllers)
                {
                    if (zoneController.Zone.IsFree)
                    {
                        zoneController.ToggleHighlight(true);
                    }
                }
            }
        }

        private void OnTrySet()
        {
            throw new NotImplementedException();
        }

        private void OnTryTributeSummon()
        {
            throw new NotImplementedException();
        }

        private void OnTryTributeSet()
        {
            throw new NotImplementedException();
        }

        private void OnClickMonsterZone(ZoneController boardZone)
        {
            if (_application.CurrentPhase.CurrentStep == GameStep.SelectingZoneToSummonMonster)
            { 
                var result = _application.CurrentPhase
                    .SummonCardOnSelectedZone(_currentSelectedCardInHand.CardInstance, boardZone.Zone);
                
                if (result)
                {
                    UpdatePlayerHand();
                    foreach (var zoneController in monsterZoneControllers)
                    {
                        zoneController.ToggleHighlight(false);
                    }
                    SetupFieldCardController(boardZone);
                }
            }
        }

        private void SetupFieldCardController(ZoneController boardZone)
        {
            if (zoneCardControllers.TryGetValue(boardZone.Zone.Position, out var fieldCardController))
            {
                fieldCardController.Init(boardZone.Zone.CardInZone);
                return;
            }
            
            var newFieldCardController = Instantiate(fieldCardPrefab, monsterCardsParent);
            newFieldCardController.Init(boardZone.Zone.CardInZone, UpdateZoomCard);
            newFieldCardController.transform.localPosition = new Vector3(boardZone.transform.localPosition.x, boardZone.transform.localPosition.y + 0.05f, 0);
            newFieldCardController.SetDefense(false);
        }
    }
}