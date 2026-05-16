using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message
{
    public interface IPosChangeMessage : IDuelMessage
    {
        uint CardCode { get; }
        byte CurrentController { get; }
        Location CurrentLocation { get; }
        byte CurrentSequence { get; }
        CardPosition PreviousPosition { get; }
        CardPosition CurrentPosition { get; }
    }
}