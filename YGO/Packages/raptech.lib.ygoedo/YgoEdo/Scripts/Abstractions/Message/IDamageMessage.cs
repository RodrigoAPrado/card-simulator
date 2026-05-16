using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message
{
    public interface IDamageMessage : IDuelMessage
    {
        byte Player { get; }
        uint Damage { get; }
    }
}