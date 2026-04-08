using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
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
        private GameObject opponentHandArea;
        [field: SerializeField]
        private HandController handController;
        
        [Header("ZoomCard")]
        [field: SerializeField]
        private CardController zoomCard;

        private GameApplication _application;
        private List<CardController> _playerHand;
        private List<CardController> _opponentHand;
 
        [Header("PoVPlayerField")]
        [field: SerializeField]
        private MainDeckController mainDeckController;
        [field: SerializeField]
        private List<ZoneController> monsterZoneControllers;
        [field: SerializeField]
        private List<CardController> monsterOnFieldControllers;
        
        [Header("OpponentField")]
        [field: SerializeField]
        private MainDeckController opponentMainDeckController;
        [field: SerializeField]
        private List<ZoneController> opponentMonsterZoneControllers;
        [field: SerializeField]
        private List<CardController> opponentMonsterOnFieldControllers;
        

        [Header("Texts")] 
        [field: SerializeField]
        private TextViewUI PhaseText; 
        [field: SerializeField]
        private TextViewUI TurnText;
        [field: SerializeField]
        private TextViewUI PoVPlayerText;
        [field: SerializeField]
        private TextViewUI OpponentPlayerText;
        
        private CardController _currentSelectedCardInHand;
        
        public void Awake()
        {
            var service = new CardLoaderService();
            var data = service.LoadCards();
            _application = new GameApplication(data);
            _application.InitializeGame();
            _playerHand = new List<CardController>();
            _opponentHand = new List<CardController>();
            mainDeckController.Init(DrawFromDeck);
            mainDeckController.SetDeckSize(_application.PointOfViewPlayer.CardsHandler.MainDeck.Count);
            opponentMainDeckController.SetDeckSize(_application.OpponentPlayer.CardsHandler.MainDeck.Count);
            _application.SubscribeToPhaseChange(OnPhaseChange);
            _application.SubscribeToTurnChange(OnTurnChange);
            PhaseText.SetText(_application.CurrentPhase.Name);
            handController.Init(OnTryNormalSummon, OnTrySet, OnTryTributeSummon, OnTryTributeSet, OnCancel);
            handController.HideAll();
            _application.SubscribeToPointOfViewChange(OnPointOfViewChange);
            SetupVisuals();
        }

        private void SetupVisuals()
        {
            zoomCard.Init();
            SetupPointOfView();
            SetupOpponent();
            PoVPlayerText.SetText($"{_application.PointOfViewPlayer.PlayerName}\n{_application.PointOfViewPlayer.CurrentLifePoints}");
            OpponentPlayerText.SetText($"{_application.OpponentPlayer.PlayerName}\n{_application.OpponentPlayer.CurrentLifePoints}");
            TurnText.SetText($"Turn: {_application.CurrentTurn}");
        }

        private void SetupPointOfView()
        {
            foreach (var zoneController in monsterZoneControllers)
            {
                var boardZone =  _application.PointOfViewPlayer.BoardHandler.MonsterZones.FirstOrDefault(x =>
                    x.Position == zoneController.Position);
                zoneController.Init(boardZone, OnClickMonsterZone);
            }
            UpdatePlayerHand();
            foreach (var monsterOnField in monsterOnFieldControllers)
            {
                monsterOnField.Init(UpdateZoomCard, ClickedOnCardOnField);
            }
            UpdatePlayerField();
        }

        private void SetupOpponent()
        {
            foreach (var zoneController in opponentMonsterZoneControllers)
            {
                var boardZone =  _application.OpponentPlayer.BoardHandler.MonsterZones.FirstOrDefault(x =>
                    x.Position == zoneController.Position);
                zoneController.Init(boardZone, OnClickOpponentMonsterZone);
            }
            UpdateOpponentHand();
            foreach (var monsterOnField in opponentMonsterOnFieldControllers)
            {
                monsterOnField.Init(UpdateZoomCard, ClickedOnOpponentCardOnField);
            }
            UpdateOpponentField();
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
                    InstantiatePlayerHandCardController(_application.PointOfViewPlayer.CardsHandler.PlayerHand[i]);
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

        private void UpdatePlayerField()
        {
            foreach (var card in monsterOnFieldControllers)
            {
                card.SetDirty();
            }
            
            foreach (var zone in monsterZoneControllers)
            {
                if (zone.Zone.IsFree)
                    continue;
                var card = monsterOnFieldControllers.FirstOrDefault(x => x.ZonePosition == zone.Position);
                card?.Enable();
                card?.UpdateCard(zone.Zone.CardInZone);
            }
            
            foreach (var card in monsterOnFieldControllers.Where(card => card.Dirty))
            {
                card.Disable();
            }
        }

        private void UpdateOpponentHand()
        {
            foreach (var card in _opponentHand)
            {
                card.SetDirty();
            }
            
            for (var i = 0; i < _application.OpponentPlayer.CardsHandler.PlayerHand.Count; i++)
            {
                if (_opponentHand.Count <= i)
                {
                    InstantiateOpponentHandCardController(_application.OpponentPlayer.CardsHandler.PlayerHand[i]);
                    continue;
                }
                _opponentHand[i].Enable();
                _opponentHand[i].UpdateCard(_application.OpponentPlayer.CardsHandler.PlayerHand[i], true);
            }

            foreach (var card in _opponentHand)
            {
                if(card.Dirty)
                    card.Disable();
            }
        }

        private void UpdateOpponentField()
        {
            foreach (var card in opponentMonsterOnFieldControllers)
            {
                card.SetDirty();
            }
            
            foreach (var zone in opponentMonsterZoneControllers)
            {
                if (zone.Zone.IsFree)
                    continue;
                var card = opponentMonsterOnFieldControllers.FirstOrDefault(x => x.ZonePosition == zone.Position);
                card?.Enable();
                card?.UpdateCard(zone.Zone.CardInZone);
            }
            
            foreach (var card in opponentMonsterOnFieldControllers.Where(card => card.Dirty))
            {
                card.Disable();
            }
        }

        private void InstantiatePlayerHandCardController(ICardInstance card)
        {
            var o = Instantiate(cardPrefab, playerHandArea.transform);
            o.Init(UpdateZoomCard, ClickedOnCardInHand);
            o.Enable();
            o.UpdateCard(card);
            _playerHand.Add(o);
        }

        private void InstantiateOpponentHandCardController(ICardInstance card)
        {
            var o = Instantiate(cardPrefab, opponentHandArea.transform);
            o.Init(UpdateZoomCard);
            o.Enable();
            o.UpdateCard(card, true);
            o.GetComponent<LayoutElement>().preferredWidth = 90;
            _opponentHand.Add(o);
        }

        private void UpdateZoomCard(ICardInstance card)
        {
            zoomCard.UpdateCard(card);
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

        private void ClickedOnCardOnField(CardController cardController)
        {
            
        }
        
        private void ClickedOnOpponentCardOnField(CardController cardController)
        {
            
        }

        private void OnPhaseChange()
        {
            PhaseText.SetText(_application.CurrentPhase.Name);
        }
        
        private void OnTurnChange()
        {
            PhaseText.SetText(_application.CurrentPhase.Name); 
            TurnText.SetText($"Turn: {_application.CurrentTurn}"); 
        }

        private void OnPointOfViewChange()
        {
            foreach (var zoneController in monsterZoneControllers)
            {
                var boardZone =  _application.PointOfViewPlayer.BoardHandler.MonsterZones.FirstOrDefault(x =>
                    x.Position == zoneController.Position);
                zoneController.UpdateZone(boardZone);
            }
            mainDeckController.SetDeckSize(_application.PointOfViewPlayer.CardsHandler.MainDeck.Count);
            UpdatePlayerHand();
            UpdatePlayerField();
            
            foreach (var zoneController in opponentMonsterZoneControllers)
            {
                var boardZone =  _application.OpponentPlayer.BoardHandler.MonsterZones.FirstOrDefault(x =>
                    x.Position == zoneController.Position);
                zoneController.UpdateZone(boardZone);
            }
            opponentMainDeckController.SetDeckSize(_application.OpponentPlayer.CardsHandler.MainDeck.Count);
            UpdateOpponentHand();
            UpdateOpponentField();
            
            PoVPlayerText.SetText($"{_application.PointOfViewPlayer.PlayerName}\n{_application.PointOfViewPlayer.CurrentLifePoints}");
            OpponentPlayerText.SetText($"{_application.OpponentPlayer.PlayerName}\n{_application.OpponentPlayer.CurrentLifePoints}");
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

        private void OnCancel()
        {
            _currentSelectedCardInHand = null;
            handController.HideAll();
        }

        private void OnClickOpponentMonsterZone(ZoneController boardZone)
        {
            
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
                    UpdatePlayerField();
                    foreach (var zoneController in monsterZoneControllers)
                    {
                        zoneController.ToggleHighlight(false);
                    }
                }
            }
        }

        public void OnClickNextPhase()
        {
            _application.CurrentPhase.GoToNextPhase();
        }
    }
}