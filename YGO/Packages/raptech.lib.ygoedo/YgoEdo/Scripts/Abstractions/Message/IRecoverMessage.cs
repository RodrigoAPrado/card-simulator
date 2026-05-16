using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message
{
    public interface IRecoverMessage : IDuelMessage
    {
        byte Player { get; }
        uint Recover { get; }
    }
}