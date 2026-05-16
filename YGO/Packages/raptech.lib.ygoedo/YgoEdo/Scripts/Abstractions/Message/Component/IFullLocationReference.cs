using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Flag;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component
{
    public interface IFullLocationReference
    {
        byte Controller { get; }
        Location Location { get; }
        uint Sequence { get; }
        CardPosition Position { get; }
    }
}