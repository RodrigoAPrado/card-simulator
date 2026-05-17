using UnityEngine;
using Ygo.Application;
using Ygo.Controller.Card;
using Ygo.Controller.Field;
using Ygo.Service;
using Ygo.View;

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
        
        private GameApplication _application;
        
        public void Awake()
        {
            var service = new DataLoaderService();
            _application = new GameApplication();
            _application.Setup();
            
            foreach (var handController in handControllers)
            {
                handController.Init();
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
            zoomCard.Init();
            
            phaseController.Init();
            confirmationController.Init();
            
            _application.Init();
        }
    }
}