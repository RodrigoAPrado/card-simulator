using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component.Command.Battle;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component.Command.Base;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component.Command.Battle
{
    public class BattleCmdToEndPhaseChoice : BattleCmdChoice, IBattleToEndPhase
    {
        public BattleCmdToEndPhaseChoice(PlayerBattleAction action, uint index) : base(action, index)
        {
        }
    }
}