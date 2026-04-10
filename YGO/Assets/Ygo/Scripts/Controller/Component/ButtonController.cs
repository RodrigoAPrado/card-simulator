using System;
using UnityEngine;
using Ygo.View;

namespace Ygo.Controller.Component
{
    public class ButtonController : MonoBehaviour
    {
        [field: SerializeField]
        private TextViewUI Label { get; set; }
        private Action _onClick;
        
        public void Init(Action onClick, string label)
        {
            _onClick = onClick;
            Label.SetText(label);
        }
        
        public void OnClick()
        {
            if(_onClick == null)
                throw new InvalidOperationException("You must init the button first.");
            
            _onClick.Invoke();
        }
    }
}