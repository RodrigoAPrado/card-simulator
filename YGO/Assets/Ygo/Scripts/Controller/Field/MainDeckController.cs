using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using Ygo.Controller.Component;
using Ygo.Core;
using Ygo.Core.Commands;
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
        
        public void Init(GameCommandBus commandBus, GameEventBus eventBus)
        {
            hoverController.Init(onClick:OnClick);
            highlightController.Init();
            eventBus.Subscribe<PointOfViewUpdateEvent>(OnPointOfViewUpdate);
            eventBus.Subscribe<PlayerDeckUpdateEvent>(OnUpdate);
            eventBus.Subscribe<PlayerShouldDrawEvent>(OnShouldDraw);
            
            _onClick = () =>
            {
                commandBus.Send(new MainDeckClickCommand(PlayerId));
            };
        }

        private void OnPointOfViewUpdate(PointOfViewUpdateEvent e)
        {
            if (pointOfView == PointOfView.Top)
            {
                PlayerId = e.OpponentId;
                return;
            }
            PlayerId = e.PointOfViewId;
        }
        
        private void OnUpdate(PlayerDeckUpdateEvent e)
        {
            if (e.PlayerId != PlayerId)
                return;
            textView.SetText(e.Deck.Count.ToString());
            highlightController.Disable();
        }

        private void OnShouldDraw(PlayerShouldDrawEvent e)
        {
            if (e.PlayerId != PlayerId)
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