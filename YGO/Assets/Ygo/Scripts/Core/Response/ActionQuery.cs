using System;
using System.Collections.Generic;
using Ygo.Core.Actions.Abstract;
using Ygo.Core.Response.Enum;

namespace Ygo.Core.Response
{
    public class ActionQuery
    {
        public Guid PlayerId { get; }
        public IList<IGameAction> Actions { get; }
        public ActionState ActionState { get; }
        public bool Success => Actions.Count > 0 && ActionState == ActionState.Success; 
        
        public ActionQuery(Guid playerId, IList<IGameAction> actions)
        {
            PlayerId = playerId;
            Actions = actions;
            ActionState = ActionState.Success;
        }

        public ActionQuery(Guid playerId, ActionState actionState)
        {
            PlayerId = playerId;
            Actions = new List<IGameAction>();
            ActionState = actionState;
        }
    }
}