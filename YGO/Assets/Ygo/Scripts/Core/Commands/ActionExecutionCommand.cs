using Ygo.Core.Actions.Abstract;
using Ygo.Core.Commands.Abstract;

namespace Ygo.Core.Commands
{
    public class ActionExecutionCommand: IGameCommand
    {
        public IGameAction Action { get; }
        public ActionExecutionCommand(IGameAction action)
        {
            Action = action;
        }
    }
}