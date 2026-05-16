using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message
{
    public interface IPlayerHintMessage : IDuelMessage
    {
        byte Player { get; }
        public PlayerHint Hint { get; }
        string Description { get; }
    }
}