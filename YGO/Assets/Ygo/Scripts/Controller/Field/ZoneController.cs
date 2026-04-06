using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Ygo.Core.Abstract;
using Ygo.Core.Board.Abstract;
using Ygo.View.Field;

namespace Ygo.Controller.Field
{
    public class ZoneController : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
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
            view.ToggleHover(false);
            view.ToggleHighlight(false);
        }

        public void ToggleHighlight(bool value)
        {
            view.ToggleHighlight(value);
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (!Zone.IsFree)
            {
                return;
            }
            _onClick?.Invoke(this);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!Zone.IsFree)
            {
                view.ToggleHover(false);
                return;
            }
            view.ToggleHover(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            view.ToggleHover(false);
        }
    }

    public enum ZoneType
    {
        MonsterZone = 0,
        SpellTrapZone = 1,
        FieldZone = 2,
        Graveyard = 3,
        MainDeck = 4,
        ExtraDeck = 5,
    }
}