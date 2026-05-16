using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message
{
    public interface IHintAttributeMessage : IDuelMessage
    {
        byte Player { get; }
        CardAttribute Attribute { get; }
    }
}