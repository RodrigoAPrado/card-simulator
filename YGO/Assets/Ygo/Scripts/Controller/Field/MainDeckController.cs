using System;
using UnityEngine;
using Ygo.Controller.Component;
using Ygo.Scripts.Core.Enum;
using Ygo.View;

namespace Ygo.Controller.Field
{
    public class MainDeckController : MonoBehaviour
    {
        [field: SerializeField] 
        private HoverController hoverController;
        [field: SerializeField] 
        private HighlightController highlightController;
        [field: SerializeField] 
        private TextViewUI textView;
        [field: SerializeField] 
        private PointOfView pointOfView;
        
        public void Init()
        {
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