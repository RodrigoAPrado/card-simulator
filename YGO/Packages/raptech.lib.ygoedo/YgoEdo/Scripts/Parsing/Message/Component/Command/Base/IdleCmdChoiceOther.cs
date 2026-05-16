using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Enum;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component.Command.Base
{
    public abstract class IdleCmdChoiceOther : IIdleCmdChoice
    {
        public IdleCmdChoiceOther(PlayerIdleAction playerIdleAction, uint index)
        {
            Action = playerIdleAction;
            Index = index;
        }

        public PlayerIdleAction Action { get; }
        public uint Index { get; }

        public override string ToString()
        {
            return $"to {Action.ToString()}...";
        }
    }
}