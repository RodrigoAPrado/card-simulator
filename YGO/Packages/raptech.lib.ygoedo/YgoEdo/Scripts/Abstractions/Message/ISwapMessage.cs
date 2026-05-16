using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message
{
    public interface ISwapMessage : IDuelMessage
    {
        ICardReference Card1 { get; }
        ICardReference Card2 { get; }
    }
}