using System;
using Ygo.Core.Actions.Abstract;

namespace Ygo.Core.Actions
{
    public class DelegatedGameAction : IGameAction
    {
        public string ActionName { get; }

        private readonly Action _action;

        public DelegatedGameAction(string actionName, Action action)
        {
            ActionName = actionName;
            _action = action;
        }
        
        public void Execute()
        {
            _action.Invoke();
        }
    }
}