using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Ygo.Scripts.View.Field;

namespace Ygo.Controller.Field
{
    public class MainDeckController : MonoBehaviour, IPointerClickHandler
    {
        [field: SerializeField] 
        private MainDeckView view;
        
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
            view.SetDeckSize(deckSize.ToString());
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            OnMainDeckClicked?.Invoke();
        }
    }
}