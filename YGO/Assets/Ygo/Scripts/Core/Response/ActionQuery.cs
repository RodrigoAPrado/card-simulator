using System;
using System.Collections.Generic;
using Ygo.Core.Actions.Abstract;
using Ygo.Core.Response.Context.Abstract;
using Ygo.Core.Response.Enum;

namespace Ygo.Core.Response
{
    public class ActionQuery
    {
        public Guid RequesterId { get; }
        public Guid ContextPlayerId { get; }
        public IList<IGameAction> Actions { get; }
        public IInteractionContext Context { get; }
        public ActionState ActionState { get; }
        public bool Success => Actions.Count > 0 && ActionState == ActionState.Success; 
        
        public ActionQuery(Guid requesterId, Guid contextPlayerId, IList<IGameAction> actions, IInteractionContext context)
        {
            RequesterId = requesterId;
            ContextPlayerId = contextPlayerId;
            Actions = actions;
            Context = context;
            ActionState = ActionState.Success;
        }

        public ActionQuery(Guid requesterId, Guid contextPlayerId, ActionState actionState)
        {
            RequesterId = requesterId;
            ContextPlayerId = contextPlayerId;
            Actions = new List<IGameAction>();
            ActionState = actionState;
        }
    }
}