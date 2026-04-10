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
        public ZonePosition Position { get; private set; }
        [field: SerializeField] 
        private ZoneView view;

        private Action<IBoardZone> _onClick;
        private IBoardZone _zone;
        
        public void Init(Action<IBoardZone> onClick)
        {
            _onClick = onClick;
            hoverController.Init(onClick: OnClick);
            view.Init();
        }

        public void SetBoardZone(IBoardZone zone)
        {
            _zone = zone;
        }

        private void OnClick()
        {
            _onClick?.Invoke(_zone);
        }
    }
}