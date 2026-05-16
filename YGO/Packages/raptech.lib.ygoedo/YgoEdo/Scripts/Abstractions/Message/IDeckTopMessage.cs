using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message
{
    public interface IDeckTopMessage : IDuelMessage
    {
        byte Player { get; }
        uint CardCode { get; }
        CardPosition Position { get; }
    }
}