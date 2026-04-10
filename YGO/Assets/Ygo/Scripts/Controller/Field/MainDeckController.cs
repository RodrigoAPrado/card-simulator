using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using Ygo.Controller.Component;
using Ygo.View;
using Ygo.View.Field;

namespace Ygo.Controller.Field
{
    public class MainDeckController : MonoBehaviour
    {
        [field: SerializeField] 
        private HoverController hoverController;
        [field: SerializeField] 
        private TextViewUI textView;
        
        public void Init()
        {
        }
        
        private void SetDeckSize(int deckSize)
        {
            textView.SetText(deckSize.ToString());
        }
        
        public void OnClick()
        {
        }

        public void OnEnter()
        {
        }

        public void OnExit()
        {
        }
    }
}