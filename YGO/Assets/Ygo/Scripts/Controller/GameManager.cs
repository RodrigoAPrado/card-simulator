using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Ygo.Application;
using Ygo.Controller.Card;
using Ygo.Controller.Data;
using Ygo.Controller.Field;
using Ygo.Controller.Handler;
using Ygo.Controller.Handler.Base;
using Ygo.Core.Duel;
using Ygo.Service;
using Ygo.View;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;

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
        private DuelInstance _duelInstance;
        private HandlerRegistry _handlerRegistry;
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
            
            foreach (var handController in handControllers)
            {
                handController.Init(_smallImageLibrary);
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

            RegisterHandlers();
        }

        public void Start()
        {
            RunDuel();
        }

        private void RegisterHandlers()
        {
            var handlers = new Dictionary<Type, IHandler>
            { { typeof(IDrawMessage), new DrawHandler(new Dictionary<int, HandController>()
            {
                {0, handControllers[0]},
                {1, handControllers[1]},
            }) } };

            _handlerRegistry = new HandlerRegistry(handlers);
        }

        private async UniTask RunDuel()
        {
            bool duelProceed;
            do
            {
                duelProceed = _duelInstance.ProceedDuel();
                bool nextMessage;
                
                do
                {
                    var duelMessage = _duelInstance.GetMessage();
                    await HandleMessage(duelMessage);
                    nextMessage = _duelInstance.NextMessage();
                } while (nextMessage);
                
            } while (duelProceed);
        }

        private async UniTask HandleMessage(IDuelMessage duelMessage)
        {
            if (duelMessage == null)
                return;

            switch (duelMessage)
            {
                case IDrawMessage drawMessage:
                    await _handlerRegistry.GetHandler<IDrawMessage>().HandleMessage(drawMessage);
                    break;
                default:
                    Debug.Log(duelMessage);
                    break;
            }
        }
    }
}