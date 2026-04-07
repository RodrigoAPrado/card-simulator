using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Ygo.Core.Abstract;
using Ygo.Core.Board.Abstract;
using Ygo.View.Field;

namespace Ygo.Controller.Field
{
    public class ZoneController : MonoBehaviour
    {
        [field: SerializeField] 
        public ZonePosition Position { get; private set; }
        [field: SerializeField] 
        private ZoneView view;
        
        public IBoardZone Zone { get; private set; }
        
        private Action<ZoneController> _onClick;
        
        
        public void Init(IBoardZone zone, Action<ZoneController> onClick)
        {
            Zone = zone;
            _onClick = onClick;
            view.Init();
        }

        public void UpdateZone(IBoardZone zone)
        {
            Zone = zone;
        }

        public void ToggleHighlight(bool value)
        {
            view.ToggleHighlight(value);
        }
        
        public void OnClick()
        {
            if (!Zone.IsFree)
            {
                return;
            }
            _onClick?.Invoke(this);
        }

        public void OnEnter()
        {
            if (!Zone.IsFree)
            {
                view.ToggleHover(false);
                return;
            }
            view.ToggleHover(true);
        }

        public void OnExit()
        {
            view.ToggleHover(false);
        }
    }
}