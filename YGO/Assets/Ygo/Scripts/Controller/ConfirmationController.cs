using System;
using UnityEngine;
using Ygo.Controller.Component;
using Ygo.View;

namespace Ygo.Controller
{
    public class ConfirmationController : MonoBehaviour
    {
        [Header("Container")]
        [field: SerializeField] 
        private GameObject confirmationContainer;
        [Header("Text")] 
        [field: SerializeField] 
        private TextViewUI messageText;
        [Header("Buttons")]
        [field: SerializeField] 
        private ButtonController acceptButton;
        [field: SerializeField] 
        private ButtonController declineButton;

        public void Init()
        {
        }
    }
}