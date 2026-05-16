using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component.Command.Base;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component.Command.Idle
{
    public interface IIdleEffectActivation : IIdleCommand
    {
        uint CardCode { get; }
        byte Controller { get; }
        uint Sequence { get; }
        Location Location { get; }
        string Description { get; }
    }
}