using System;
using System.Collections.Generic;
using Ygo.Core.Actions.Abstract;
using Ygo.Core.Response.Enum;

namespace Ygo.Core.Response
{
    public class ActionResult
    {
        public Guid ContextPlayerId { get; }
        public ActionState ActionState { get; }

        public ActionResult(Guid contextContextPlayerId, ActionState actionState)
        {
            ContextPlayerId = contextContextPlayerId;
            ActionState = actionState;
        }
    }
}