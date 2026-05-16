using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message
{
    public interface IChainSolvedMessage : IDuelMessage
    {
        byte ChainCount { get; }
    }
}