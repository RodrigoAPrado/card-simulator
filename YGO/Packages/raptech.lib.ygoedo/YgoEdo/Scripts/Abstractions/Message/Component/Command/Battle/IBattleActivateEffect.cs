using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component.Command.Base;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component.Command.Battle
{
    public interface IBattleActivateEffect : IBattleCommand
    {
        uint CardCode { get; }
        byte Controller { get; }
        Location Location { get; }
        uint Sequence { get; }
        string Description { get; }
    }
}