using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message
{
    public interface ICardHintMessage : IDuelMessage
    {
        public IFullLocationReference LocationReference { get; }
        public DuelCardHint CardHint { get; }
        public string Description { get; }
    }
}