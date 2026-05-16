using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card.Flag;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component
{
    public interface IDrawnCard
    {
        uint CardCode { get; }
        CardPosition CardPosition { get; }
    }
}