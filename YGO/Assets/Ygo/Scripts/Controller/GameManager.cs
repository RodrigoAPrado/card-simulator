using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Ygo.Application;
using Ygo.Controller.Card;
using Ygo.Controller.Field;
using Ygo.Core.Abstract;
using Ygo.Core.Enums;
using Ygo.Core.Events;
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
            var service = new CardLoaderService();
            var data = service.LoadCards();
            _application = new GameApplication(data);
            _application.Setup();

            var registry = new CardControllerRegistry();
            
            foreach (var handController in handControllers)
            {
                handController.Init(
                    _application.GameCommandBus, 
                    _application.GameEventBus, 
                    _application.TurnContext, 
                    registry,
                    UpdateZoomCard
                    );
            }
            
            foreach (var fieldController in fieldControllers)
            {
                fieldController.Init(
                    _application.GameCommandBus, 
                    _application.GameEventBus, 
                    _application.TurnContext, 
                    registry,
                    UpdateZoomCard
                    );
            }

            foreach (var mainDeckController in mainDeckControllers)
            {
                mainDeckController.Init(
                    _application.GameCommandBus, 
                    _application.GameEventBus, 
                    _application.TurnContext
                    );
            }
            
            actionController.Init(_application.GameCommandBus, _application.GameEventBus, registry);
            zoomCard.Init();
            
            phaseController.Init(_application.GameCommandBus, _application.GameEventBus);
            
            _application.GameEventBus.Subscribe<PlayerInfoUpdateEvent>(OnPlayerInfoUpdate);
            //_application.GameEventBus.Subscribe<PlayerTakenBattleDamageEvent>();
            _application.GameEventBus.Subscribe<TurnChangeEvent>(OnTurnChange);
            _application.GameEventBus.Subscribe<CommandDeniedEvent>(OnCommandDenied);
            
            _application.Init();
        }

        private void UpdateZoomCard(ICardInstance card, bool hidden)
        {
            zoomCard.UpdateCard(card, hidden);
        }

        private void OnPlayerInfoUpdate(PlayerInfoUpdateEvent e)
        {
            poVPlayerText.SetText($"{e.PlayerName}\n{e.PlayerLifePoint}");
            opponentPlayerText.SetText($"{e.OpponentName}\n{e.OpponentLifePoint}");
        }

        private void OnTurnChange(TurnChangeEvent e)
        {
            turnText.SetText($"Turn: {e.TurnIndex}");
        }

        private void OnCommandDenied(CommandDeniedEvent e)
        {
            Debug.LogWarning($"{e.CommandType} is denied because {e.ActionState}");
        }
    }
}