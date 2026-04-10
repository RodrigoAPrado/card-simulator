using System;
using System.Collections.Generic;
using Ygo.Core.Actions.Abstract;
using Ygo.Core.Response.Enum;

namespace Ygo.Core.Response
{
    public class ActionResult
    {
        public Guid PlayerId { get; }
        public ActionState ActionState { get; }

        public ActionResult(Guid playerId, ActionState actionState)
        {
            PlayerId = playerId;
            ActionState = actionState;
        }
    }
}