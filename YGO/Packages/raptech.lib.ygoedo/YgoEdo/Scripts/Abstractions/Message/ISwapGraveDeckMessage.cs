using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message
{
    public interface ISwapGraveDeckMessage : IDuelMessage
    {
        byte Player { get; }
        uint ExtraSize { get; }
        byte[] Data { get; }
    }
}