using Ygo.Core.Events.Abstract;
using Ygo.Core.Response.Enum;

namespace Ygo.Core.Events
{
    public class CommandDeniedEvent : IGameEvent
    {
        public CommandType CommandType { get; }
        public ActionState ActionState { get; }
        
        public CommandDeniedEvent(CommandType commandType, ActionState actionState)
        {
            CommandType = commandType;
            ActionState = actionState;
        }
    }

    public enum CommandType
    {
        MainDeckCLicked,
        CardInHandClicked,
        CardOnFieldClicked,
        ZoneClicked,
        NextPhaseClicked,
    }
}