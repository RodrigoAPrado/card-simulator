using System;
using UnityEngine;
using Ygo.Controller.Card;
using Ygo.Controller.Component;
using Ygo.Core;
using Ygo.Core.Actions.Abstract;
using Ygo.Core.Commands;
using Ygo.Core.Enums;
using Ygo.Core.Events;
using Ygo.Core.Events.Abstract;
using Ygo.Core.Response;
using Ygo.Core.Response.Context;
using Ygo.Core.Response.Context.Abstract;

namespace Ygo.Controller
{
    public class ActionController : MonoBehaviour
    {
        [Header("Actions")] 
        [field: SerializeField]
        private ButtonController[] buttons;
        [field: SerializeField]
        private Transform handPosition;
        [field: SerializeField]
        private Transform frontRowPosition;
        [field: SerializeField]
        private Transform backRowPosition;

        private Guid _requesterId;
        private CardControllerRegistry _registry;
        private GameCommandBus _commandBus;
        
        public void Init(GameCommandBus commandBus, GameEventBus eventBus, CardControllerRegistry registry)
        {
            eventBus.Subscribe<AvailableActionsEvent>(OnAvailableActions);
            eventBus.Subscribe<PointOfViewUpdateEvent>(OnPointOfViewUpdate);
            _registry = registry;
            _commandBus = commandBus;
            HideAll();
        }

        private void HideAll()
        {
            foreach (var button in buttons)
            {
                button.Disable(true);
            }
        }

        private void OnPointOfViewUpdate(PointOfViewUpdateEvent e)
        {
            _requesterId = e.PointOfViewId;
        }

        private void OnAvailableActions(AvailableActionsEvent e)
        {
            foreach (var button in buttons)
            {
                button.SetIsDirty();    
            }
            
            for (int i = 0; i < e.Actions.Actions.Count; i++)
            {
                if(buttons.Length <= i)
                    throw new InvalidOperationException("Not enough buttons");
                var action = e.Actions.Actions[i];
                var button = buttons[i];
                button.Init(() =>
                {
                    OnClick(e.Actions.ContextPlayerId, action);
                }, action.ActionName);
            }
            
            foreach (var button in buttons)
            {
                if(button.IsDirty)
                    button.Disable(true);
            }

            transform.position = SetPositionByContext(e.Actions.Context);
        }

        private Vector2 SetPositionByContext(IInteractionContext context)
        {
            if (context is CardInteractionContext cardContext)
            {
                var cardLocation = cardContext.Card.Location;
                switch (cardLocation)
                {
                    case CardLocation.Hand:
                        return new Vector2(_registry.Get(cardContext.Card).gameObject.transform.position.x,
                            handPosition.position.y);
                    case CardLocation.FieldZone
                        or CardLocation.LeftCenterMonsterZone
                        or CardLocation.LeftMostMonsterZone
                        or CardLocation.RightCenterMonsterZone
                        or CardLocation.RightMostMonsterZone
                        or CardLocation.MiddleCenterMonsterZone:
                        return new Vector2(Camera.main.WorldToScreenPoint(
                                _registry.Get(cardContext.Card).gameObject.transform.localPosition).x,
                            frontRowPosition.position.y);
                }
            }

            /*
            if (context is MainDeckInteractionContext)
            {
                return new Vector2(mainDeckPosition.position.x, backRowPosition.position.y);
            }*/
            throw new InvalidOperationException("Invalid context");
        }

        private void OnClick(Guid ownerId, IGameAction gameAction)
        {
            HideAll();
            _commandBus.Send(new ActionExecutionCommand(_requesterId, ownerId, gameAction));
        }
    }
}