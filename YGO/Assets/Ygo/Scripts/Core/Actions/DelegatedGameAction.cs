using System;
using Ygo.Core.Actions.Abstract;

namespace Ygo.Core.Actions
{
    public class DelegatedGameAction : IGameAction
    {
        public string ActionName => "Delegated Game Action";

        private readonly Action _action;

        public DelegatedGameAction(Action action)
        {
            _action = action;
        }
        
        public void Execute()
        {
            _action.Invoke();
        }
    }
}