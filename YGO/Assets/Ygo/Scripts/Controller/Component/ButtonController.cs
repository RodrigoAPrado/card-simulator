using System;
using UnityEngine;
using Ygo.Core.Actions.Abstract;
using Ygo.View;

namespace Ygo.Controller.Component
{
    public class ButtonController : MonoBehaviour
    {
        [field: SerializeField]
        private TextViewUI Label { get; set; }
        
        public bool IsDirty { get; private set; }
        private Action<IGameAction> _onClick;
        private IGameAction _action;
        private bool _disabled;
        
        public void Init(IGameAction action, Action<IGameAction> onClick, string label)
        {
            gameObject.SetActive(true);
            IsDirty = false;
            _disabled = false;
            _action = action;
            _onClick = onClick;
            Label.SetText(label);
        }

        public void Disable(bool deactivate)
        {
            IsDirty = false;
            if(deactivate)
                gameObject.SetActive(false);
            _disabled = true;
        }

        public void SetIsDirty()
        {
            IsDirty = true;
        }

        public void OnClick()
        {
            if (_disabled)
                return;
            _onClick?.Invoke(_action);
        }
    }
}