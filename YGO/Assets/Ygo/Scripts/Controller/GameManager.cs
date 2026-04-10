using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Ygo.Application;
using Ygo.Controller.Card;
using Ygo.Controller.Field;
using Ygo.Core.Abstract;
using Ygo.Core.Enums;
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
        
        [Header("ActionMenu")]
        [field: SerializeField]
        private ActionController actionController;
        
        [Header("ZoomCard")]
        [field: SerializeField]
        private CardController zoomCard;
        
        [Header("Texts")] 
        [field: SerializeField]
        private TextViewUI phaseText; 
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
            _application.InitializeGame();
            
            foreach (var handController in handControllers)
            {
                handController.Init(_application.GameCommandBus, _application.GameEventBus, UpdateZoomCard);
            }
            
            foreach (var fieldController in fieldControllers)
            {
                fieldController.Init(_application.GameCommandBus, _application.GameEventBus, UpdateZoomCard);
            }
            
            actionController.Init(_application.GameCommandBus);
            
            zoomCard.Init();
            poVPlayerText.SetText($"{_application.PointOfViewPlayer.PlayerName}\n{_application.PointOfViewPlayer.CurrentLifePoints}");
            opponentPlayerText.SetText($"{_application.OpponentPlayer.PlayerName}\n{_application.OpponentPlayer.CurrentLifePoints}");
            turnText.SetText($"Turn: {_application.CurrentTurn}");
        }

        private void UpdateZoomCard(ICardInstance card)
        {
            zoomCard.UpdateCard(card);
        }
    }
}