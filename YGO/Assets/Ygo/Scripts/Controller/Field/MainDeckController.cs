using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using Ygo.View;
using Ygo.View.Field;

namespace Ygo.Controller.Field
{
    public class MainDeckController : MonoBehaviour
    {
        [FormerlySerializedAs("view")] [field: SerializeField] 
        private TextViewUI textView;

        [field: SerializeField] 
        private ZoneView zoneView;

        private Action _onClick;
        
        public void Init(Action onClick)
        {
            _onClick = onClick;
        }
        
        public void SetDeckSize(int deckSize)
        {
            textView.SetText(deckSize.ToString());
        }
        
        public void OnClick()
        {
            _onClick?.Invoke();
        }

        public void OnEnter()
        {
            zoneView.ToggleHover(true);
        }

        public void OnExit()
        {
            zoneView.ToggleHover(false);
        }
    }
}