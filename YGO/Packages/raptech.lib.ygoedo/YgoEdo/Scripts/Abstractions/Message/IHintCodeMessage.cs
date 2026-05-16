using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message
{
    public interface IHintCodeMessage : IDuelMessage
    {
        byte Player { get; }
        uint CardCode { get; }
    }
}