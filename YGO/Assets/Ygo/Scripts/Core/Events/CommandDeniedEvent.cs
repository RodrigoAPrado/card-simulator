using Ygo.Core.Events.Abstract;

namespace Ygo.Core.Events
{
    public class CommandDeniedEvent : IGameEvent
    {
        public CommandType CommandType { get; }
        public DenialReason DenialReason { get; }
        
        public CommandDeniedEvent(CommandType commandType, DenialReason denialReason)
        {
            CommandType = commandType;
            DenialReason = denialReason;
        }
    }

    public enum CommandType
    {
        MainDeckCLicked
    }

    public enum DenialReason
    {
        NotInDrawPhase
    }
}