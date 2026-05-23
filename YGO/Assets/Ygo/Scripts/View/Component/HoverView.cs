using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace Ygo.View.Component
{
    public class HoverView : MonoBehaviour
    {
        [field: SerializeField]
        private Transform content;

        private bool _enabled;
        
        public void ToggleEnable(bool value)
        {
            _enabled = value;
            if (!_enabled)
            {
                content.DOKill();
                content.localScale = Vector3.one;
            }
        }
        

        public void OnEnter()
        {
            if (!_enabled)
                return;
            content.DOKill();
            content.localScale = Vector3.one;
            content.DOScale(new Vector3(1.1f, 1.1f, 1.0f), 0.3f);
        }

        public void OnExit()
        {
            if (!_enabled)
                return;
            content.DOKill();
            content.localScale = new Vector3(1.1f, 1.1f, 1.0f);
            content.DOScale(Vector3.one, 0.3f);
        }
    }
}