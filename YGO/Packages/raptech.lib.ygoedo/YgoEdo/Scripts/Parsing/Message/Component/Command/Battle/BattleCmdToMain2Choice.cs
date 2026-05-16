using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component.Command.Battle;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component.Command.Base;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component.Command.Battle
{
    public class BattleCmdToMain2Choice : BattleCmdChoice, IBattleToMainPhase2
    {
        public BattleCmdToMain2Choice(PlayerBattleAction action, uint index) : base(action, index)
        {
        }

        public override string ToString()
        {
            return $"to {Action.ToString()}...";
        }
    }
}