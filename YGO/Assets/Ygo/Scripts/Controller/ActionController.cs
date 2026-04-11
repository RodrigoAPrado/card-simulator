using System;
using UnityEngine;
using Ygo.Controller.Card;
using Ygo.Controller.Component;
using Ygo.Core;
using Ygo.Core.Actions.Abstract;
using Ygo.Core.Commands;
using Ygo.Core.Events;
using Ygo.Core.Response;

namespace Ygo.Controller
{
    public class ActionController : MonoBehaviour
    {
        [Header("Actions")] 
        [field: SerializeField]
        private ButtonController[] buttons;
        
        private CardControllerRegistry _registry;
        private GameCommandBus _commandBus;
        
        public void Init(GameCommandBus commandBus, GameEventBus eventBus, CardControllerRegistry registry)
        {
            eventBus.Subscribe<AvailableActionsEvent>(OnAvailableActions);
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

        private void DeactivateAll()
        {
            foreach (var button in buttons)
            {
                button.Disable(false);
            }
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
                button.Init(action, OnClick, action.ActionName);
            }
            
            foreach (var button in buttons)
            {
                if(button.IsDirty)
                    button.Disable(true);
            }
        }

        private void OnClick(IGameAction gameAction)
        {
            DeactivateAll();
            _commandBus.Send(new ActionExecutionCommand(gameAction));
        }
    }
}