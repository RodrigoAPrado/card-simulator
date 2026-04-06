using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Ygo.View;

namespace Ygo.Controller.Field
{
    public class MainDeckController : MonoBehaviour, IPointerClickHandler
    {
        [field: SerializeField] 
        private TextView view;

        private Action _onClick;
        
        public void Init(Action onClick)
        {
            _onClick = onClick;
        }
        
        public void SetDeckSize(int deckSize)
        {
            view.SetText(deckSize.ToString());
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            _onClick?.Invoke();
        }
    }
}