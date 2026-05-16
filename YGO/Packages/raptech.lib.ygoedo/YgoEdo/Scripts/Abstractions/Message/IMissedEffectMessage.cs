using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message
{
    public interface IMissedEffectMessage : IDuelMessage
    {
        IFullLocationReference LocationReference { get; }
        uint CardCode { get; }
    }
}