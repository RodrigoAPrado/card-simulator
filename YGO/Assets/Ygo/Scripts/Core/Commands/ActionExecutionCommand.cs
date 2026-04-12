using System;
using Ygo.Core.Actions.Abstract;
using Ygo.Core.Commands.Abstract;

namespace Ygo.Core.Commands
{
    public class ActionExecutionCommand: IGameCommand
    {

        public Guid RequesterId { get; }
        public Guid OwnerId { get; }
        public IGameAction Action { get; }
        public ActionExecutionCommand(Guid requesterId, Guid playerId, IGameAction action)
        {
            RequesterId = requesterId;
            OwnerId = playerId;
            Action = action;
        }
    }
}