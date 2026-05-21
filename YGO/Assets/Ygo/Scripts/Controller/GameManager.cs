using UnityEngine;
using Ygo.Application;
using Ygo.Controller.Card;
using Ygo.Controller.Data;
using Ygo.Controller.Field;
using Ygo.Core.Duel;
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
        private FieldController[] fieldControllers;
        [Header("MainDeck")]
        [field: SerializeField]
        private MainDeckController[] mainDeckControllers;
        [Header("ActionMenu")]
        [field: SerializeField]
        private ActionController actionController;
        [Header("ZoomCard")]
        [field: SerializeField]
        private CardController zoomCard;
        [Header("Phase")] 
        [field: SerializeField]
        private PhaseController phaseController;
        [Header("ConfirmationModal")]
        [field: SerializeField]
        private ConfirmationController confirmationController;
        
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
                handController.Init(_smallImageLibrary, _duelInstance.CardsInDuel, UpdateZoomCard);
            }
            
            foreach (var fieldController in fieldControllers)
            {
                fieldController.Init();
            }

            foreach (var mainDeckController in mainDeckControllers)
            {
                mainDeckController.Init();
            }
            
            actionController.Init();
            zoomCard.Init(_croppedImageLibrary);
            phaseController.Init();
            confirmationController.Init();
            announcementController.Init();
        }

        public void Start()
        {
            _ = _duelInstance.RunDuel();
        }

        private void UpdateZoomCard(ICardData cardData, bool hidden)
        {
            zoomCard.UpdateCard(cardData);
        }
    }
}