using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message
{
    public interface IBattleMessage : IDuelMessage
    {
        IBattleReference Attacker { get; }
        IBattleReference Defender { get; }
    }
}