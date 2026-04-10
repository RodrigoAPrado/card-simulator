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
        private bool poVPlayer;

        private Action _onClick;
        
        public void Init(GameCommandBus commandBus, GameEventBus eventBus)
        {
            hoverController.Init(onClick:OnClick);
            highlightController.Init();
            eventBus.Subscribe<PlayerDeckUpdateEvent>(OnUpdate);
            eventBus.Subscribe<PlayerShouldDrawEvent>(OnShouldDraw);
            
            _onClick = () =>
            {
                commandBus.Send(new MainDeckClickCommand(poVPlayer));
            };
        }
        
        private void OnUpdate(PlayerDeckUpdateEvent e)
        {
            if (e.Pov != poVPlayer)
                return;
            textView.SetText(e.Deck.Count.ToString());
            highlightController.Disable();
        }

        private void OnShouldDraw(PlayerShouldDrawEvent e)
        {
            if (!poVPlayer)
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