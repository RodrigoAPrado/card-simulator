using System.Collections.Generic;
using UnityEngine;
using Ygo.Application;
using Ygo.Controller.Card;
using Ygo.Controller.Data;
using Ygo.Controller.Field;
using Ygo.Controller.Modal;
using Ygo.Core.Duel;
using Ygo.Scripts.Core.Enum;
using Ygo.Scripts.Core.Model;
using Ygo.View;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card;

namespace Ygo.Controller
{
    public class GameManager : MonoBehaviour
    {
        [Header("Hands")] 
        [field: SerializeField]
        private HandController[] handControllers;
        [Header("Field")] 
        [field: SerializeField]
        private FieldController fieldController;
        [Header("MainDeck")]
        [field: SerializeField]
        private DeckController[] mainDeckControllers;
        [Header("ActionMenu")]
        [field: SerializeField]
        private ActionController actionController;
        [Header("MovementAnimator")]
        [field: SerializeField]
        private MovementController movementController;
        [Header("ZoomCard")]
        [field: SerializeField]
        private CardController zoomCard;
        [Header("Phase")] 
        [field: SerializeField]
        private PhaseController phaseController;
        [Header("ConfirmationModal")]
        [field: SerializeField]
        private ConfirmationController confirmationController;
        [Header("CardSelectionModal")]
        [field: SerializeField]
        private CardSelectionModalController cardSelectionModal;
        [Header("ConfirmEffectModal")]
        [field: SerializeField]
        private ConfirmEffectModalController confirmEffectModal;
        
        [Header("Texts")]  
        [field: SerializeField]
        private TextViewUI turnText;
        [field: SerializeField]
        private TextViewUI poVPlayerText;
        [field: SerializeField]
        private TextViewUI opponentPlayerText;
        [field: SerializeField]
        private AnnouncementController announcementController;
        
        private GameApplication _application;
        private DuelInstance _duelInstance;
        private CardImageLibrary _smallImageLibrary;
        private CardImageLibrary _croppedImageLibrary;
        
        public void Awake()
        {
            _application = new GameApplication();
            _application.Setup();
            _duelInstance = _application.Init();
            _smallImageLibrary = new CardImageLibrary(_duelInstance.CardsInDuel, true);
            _smallImageLibrary.LoadImages();
            _croppedImageLibrary = new CardImageLibrary(_duelInstance.CardsInDuel, false);
            _croppedImageLibrary.LoadImages();
            
            foreach (var handController in handControllers)
            {
                handController.Init(_duelInstance.EventQueue, _smallImageLibrary, UpdateZoomCard);
            }
            
            foreach (var deckController in mainDeckControllers)
            {
                var player = deckController.PointOfView == PointOfView.Player
                    ? _duelInstance.DuelData.PlayerId
                    : _duelInstance.DuelData.PlayerId + 1;

                if (player > 1)
                    player = 0;

                deckController.Init(
                    player == 0
                        ? _duelInstance.DuelData.Duelist0.MainDeck.Count
                        : _duelInstance.DuelData.Duelist1.MainDeck.Count, _duelInstance.EventQueue);
            }
            
            fieldController.Init(_duelInstance, _smallImageLibrary);
            actionController.Init();
            zoomCard.Init(_croppedImageLibrary);
            phaseController.Init();
            confirmationController.Init();
            announcementController.Init(_duelInstance.EventQueue);
            cardSelectionModal.Init(_duelInstance, _smallImageLibrary);
            confirmEffectModal.Init(_smallImageLibrary);
            movementController.Init(_duelInstance, handControllers, fieldController, mainDeckControllers);
        }

        public void Start()
        {
            _ = _duelInstance.RunDuel();
        }

        private void UpdateZoomCard(CardModel cardModel, bool hidden)
        {
            zoomCard.UpdateCard(cardModel.Data);
        }
    }
}