using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message
{
    public interface IChainingMessage : IDuelMessage
    {
        uint CardCode { get; }
        IFullLocationReference LocationReference { get; }
        byte ActivationPlayer { get; }
        Location ActivationLocation { get; }
        uint ActivationSequence { get; }
        string Description { get; }
        uint ChainSize { get; }
    }
}