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
        
        public event Action OnMainDeckClicked;
        
        public void SubscribeToMainDeckClicked(Action action)
        {
            OnMainDeckClicked += action;
        }

        public void UnsubscribeFromMainDeckClicked(Action action)
        {
            OnMainDeckClicked -= action;
        }

        public void SetDeckSize(int deckSize)
        {
            view.SetText(deckSize.ToString());
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            OnMainDeckClicked?.Invoke();
        }
    }
}