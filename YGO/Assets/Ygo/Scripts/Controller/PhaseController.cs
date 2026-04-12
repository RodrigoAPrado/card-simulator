using System;
using UnityEngine;
using Ygo.Controller.Component;
using Ygo.Core;
using Ygo.Core.Commands;
using Ygo.Core.Events;
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
        
        public void Init(GameCommandBus commandBus, GameEventBus eventBus) 
        {
            eventBus.Subscribe<PointOfViewUpdateEvent>(OnPointOfViewUpdate);
            eventBus.Subscribe<PhaseBeginEvent>(OnPhaseUpdate);
            nextPhaseButton.Init(() =>
            {
                commandBus.Send(new NextPhaseClickCommand(_requesterId));
            }, "Next Phase");
        }
        
        private void OnPointOfViewUpdate(PointOfViewUpdateEvent e)
        {
            _requesterId = e.PointOfViewId;
        }

        private void OnPhaseUpdate(PhaseBeginEvent e)
        {
            phaseText.SetText(e.Phase.ToString());
        }
    }
}