using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message
{
    public interface IHintNumberMessage : IDuelMessage
    {
        byte Player { get; }
        ulong Number { get; }
    }
}