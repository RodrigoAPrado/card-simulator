using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using Ygo.Controller.Component;
using Ygo.Core;
using Ygo.Core.Commands;
using Ygo.Core.Enums;
using Ygo.Core.Events;
using Ygo.View;
using Ygo.View.Field;

namespace Ygo.Controller.Field
{
    public class MainDeckController : MonoBehaviour
    {
        [field: SerializeField] 
        private HoverController hoverController;
        [field: SerializeField] 
        private HighlightController highlightController;
        [field: SerializeField] 
        private TextViewUI textView;
        [field: SerializeField] 
        private PointOfView pointOfView;
        private Guid PlayerId { get; set; }

        private Action _onClick;
        private TurnContext _context;
        private CardsHandler _cardsHandler;
        
        public void Init(GameCommandBus commandBus, GameEventBus eventBus, TurnContext context)
        {
            hoverController.Init(onClick:OnClick);
            highlightController.Init();
            eventBus.Subscribe<PointOfViewUpdateEvent>(OnPointOfViewUpdate);
            eventBus.Subscribe<CardDrawnEvent>(OnCardDrawn);
            eventBus.Subscribe<PhaseBeginEvent>(OnPhaseBegin);
            
            _onClick = () =>
            {
                commandBus.Send(new MainDeckClickCommand(PlayerId));
            };
            _context = context;
        }

        private void OnPointOfViewUpdate(PointOfViewUpdateEvent e)
        {
            if (pointOfView == PointOfView.Top)
            {
                PlayerId = e.OpponentId;
                SetCardsHandler();
                return;
            }
            PlayerId = e.PointOfViewId;
            SetCardsHandler();
        }
        
        private void SetCardsHandler()
        {
            var player = _context.Players.FirstOrDefault(x => x.Id == PlayerId);
            if(player == null)
                throw new InvalidOperationException("Player not found");
            _cardsHandler = player.CardsHandler;
        }
        
        private void OnCardDrawn(CardDrawnEvent e)
        {
            if (e.PlayerId != PlayerId)
                return;
            textView.SetText(_cardsHandler.MainDeck.Count.ToString());
            highlightController.Disable();
        }

        private void OnPhaseBegin(PhaseBeginEvent e)
        {
            if (e.TurnPlayer != PlayerId)
                return;

            if (e.Phase != GamePhase.DrawPhase)
                return;
            
            highlightController.Enable();
        }
        
        public void OnClick()
        {
            _onClick?.Invoke();
        }

        public void OnEnter()
        {
        }

        public void OnExit()
        {
        }
    }
}