using System;
using Ygo.Core.Actions.Abstract;
using Ygo.Core.Commands.Abstract;

namespace Ygo.Core.Commands
{
    public class ActionExecutionCommand: IGameCommand
    {
        public Guid PlayerId { get; }
        public IGameAction Action { get; }
        public ActionExecutionCommand(Guid playerId, IGameAction action)
        {
            PlayerId = playerId;
            Action = action;
        }
    }
}