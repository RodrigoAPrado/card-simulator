using System;
using UnityEngine;
using Ygo.Controller.Component;
using Ygo.Core;
using Ygo.Core.Commands;
using Ygo.Core.Events;
using Ygo.Core.Interaction.Abstract;
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
        
        private Guid _requesterId;

        public void Init(GameCommandBus commandBus, GameEventBus eventBus)
        {
            eventBus.Subscribe<InteractionStateSetEvent>(OnInteractionStateSet);
            eventBus.Subscribe<PointOfViewUpdateEvent>(OnPointOfViewUpdate);
            acceptButton.Init(() =>
            {
                commandBus.Send(new PlayerConfirmationCommand(_requesterId, true));
                confirmationContainer.SetActive(false);
            }, "Yes");
            
            declineButton.Init(() =>
            {
                commandBus.Send(new PlayerConfirmationCommand(_requesterId, false));
                confirmationContainer.SetActive(false);
            }, "No");
            confirmationContainer.SetActive(false);
        }

        private void OnPointOfViewUpdate(PointOfViewUpdateEvent e)
        {
            _requesterId = e.PointOfViewId;
        }

        private void OnInteractionStateSet(InteractionStateSetEvent e)
        {
            if (e.RequesterId != _requesterId)
                return;
            if (e.InteractionState is not ConfirmationState confirmationCommand) 
                return;
           
            confirmationContainer.SetActive(true);
            messageText.SetText(confirmationCommand.Message);
        }
    }
}