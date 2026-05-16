using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message
{
    public interface IAttackMessage : IDuelMessage
    {
        IFullLocationReference Attacker { get; }
        IFullLocationReference Target { get; }
    }
}