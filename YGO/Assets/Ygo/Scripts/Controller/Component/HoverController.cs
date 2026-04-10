using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Ygo.Controller.Component
{
    public class HoverController : MonoBehaviour
    {
        [field: SerializeField] 
        private GameObject hoverImage;
        private Action _onClick;
        private Action _onEnter;
        private Action _onExit;
        private bool _doNotHover;
        
        public void Init(Action onClick = null, Action onEnter = null, Action onExit = null, bool doNotHover = false)
        {
            _onClick = onClick;
            _onEnter = onEnter;
            _onExit = onExit;
            _doNotHover = doNotHover;
            hoverImage.SetActive(false);
        }

        public void OnClick()
        {
            _onClick?.Invoke();
        }

        public void OnEnter()
        {
            hoverImage.SetActive(true && !_doNotHover);
            _onEnter?.Invoke();
        }

        public void OnExit()
        {
            hoverImage.SetActive(false);
            _onExit?.Invoke();
        }
    }
}