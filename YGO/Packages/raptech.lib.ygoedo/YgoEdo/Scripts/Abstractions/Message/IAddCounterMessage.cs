using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message
{
    public interface IAddCounterMessage : IDuelMessage
    {
        CounterType CounterType { get; }
        byte Player { get; }
        Location Location { get; }
        byte Sequence { get; }
        ushort Count { get; }
    }
}