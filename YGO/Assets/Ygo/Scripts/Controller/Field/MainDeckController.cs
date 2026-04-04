using System;
using UnityEngine;
using Ygo.Scripts.View.Field;

namespace Ygo.Controller.Field
{
    public class MainDeckController : MonoBehaviour
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
        
        public void OnOnMainDeckClicked()
        {
            OnMainDeckClicked?.Invoke();
        }

        public void SetDeckSize(int deckSize)
        {
            view.SetDeckSize(deckSize.ToString());
        }
    }
}