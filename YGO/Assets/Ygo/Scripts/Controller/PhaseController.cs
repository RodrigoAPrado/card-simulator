using System;
using UnityEngine;
using Ygo.Controller.Component;
using Ygo.View;

namespace Ygo.Controller
{
    public class PhaseController : MonoBehaviour
    {
        [field: SerializeField]
        private TextViewUI phaseText;
        [field: SerializeField] 
        private ButtonController nextPhaseButton;
        private Guid _requesterId;
        
        public void Init() 
        {
        }
        
        private void OnPointOfViewUpdate()
        {
        }

        private void OnPhaseUpdate()
        {
        }
    }
}