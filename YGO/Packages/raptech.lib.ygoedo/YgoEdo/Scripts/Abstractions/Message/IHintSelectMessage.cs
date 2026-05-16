using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message
{
    public interface IHintSelectMessage : IDuelMessage
    {
        byte Player { get; }
        string Description { get; }
    }
}