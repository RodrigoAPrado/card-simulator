using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component.Command.Base;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component.Command.Base
{
    public abstract class BattleCmdChoice : IBattleCommand
    {
        protected BattleCmdChoice(PlayerBattleAction action, uint sequence)
        {
            Action = action;
            Index = sequence;
        }

        public PlayerBattleAction Action { get; }
        public uint Index { get; }
    }
}