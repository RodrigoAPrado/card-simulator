using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message
{
    public interface ICardTargetMessage : IDuelMessage
    {
        IFullLocationReference Card { get; }
        IFullLocationReference Target { get; }
    }
}