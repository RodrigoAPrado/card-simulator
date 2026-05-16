using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component.Command.Base;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component.Command.Battle
{
    public interface IBattleAttack : IBattleCommand
    {
        uint CardCode { get; }
        byte Controller { get; }
        Location Location { get; }
        uint Sequence { get; }
        bool DirectAttack { get; }
    }
}