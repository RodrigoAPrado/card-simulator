using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message
{
    public interface IEquipMessage : IDuelMessage
    {
        IFullLocationReference Equipment { get; }
        IFullLocationReference Target { get; }
    }
}