using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message
{
    public interface IHintOpSelectedMessage : IDuelMessage
    {
        byte Player { get; }
        string Description { get; }
    }
}