using Ygo.Core.Events.Abstract;
using Ygo.Core.Response.Enum;

namespace Ygo.Core.Events
{
    public class CommandDeniedEvent : IGameEvent
    {
        public CommandType CommandType { get; }
        public GameStateResult DenialReason { get; }
        
        public CommandDeniedEvent(CommandType commandType, GameStateResult denialReason)
        {
            CommandType = commandType;
            DenialReason = denialReason;
        }
    }

    public enum CommandType
    {
        MainDeckCLicked
    }
}