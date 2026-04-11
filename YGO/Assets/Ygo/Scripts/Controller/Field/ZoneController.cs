using System;
using UnityEngine;
using Ygo.Controller.Component;
using Ygo.Core;
using Ygo.Core.Board.Abstract;
using Ygo.View.Field;

namespace Ygo.Controller.Field
{
    public class ZoneController : MonoBehaviour
    {
        [field: SerializeField] 
        private HoverController hoverController;
        [field: SerializeField] 
        private HighlightController highlightController;
        
        [field: SerializeField] 
        public ZonePosition Position { get; private set; }
        [field: SerializeField] 
        private ZoneView view;

        private Action<IBoardZone> _onClick;
        public IBoardZone Zone { get; private set; }
        
        public void Init(Action<IBoardZone> onClick)
        {
            _onClick = onClick;
            hoverController.Init(onClick: OnClick);
            view.Init();
            highlightController.Init();
        }

        public void SetBoardZone(IBoardZone zone)
        {
            Zone = zone;
        }

        public void ToggleHighlight(bool value)
        {
            if(value)
                highlightController.Enable();
            else
                highlightController.Disable();
        }

        private void OnClick()
        {
            _onClick?.Invoke(Zone);
        }
    }
}