namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component
{
    public interface ICardReference
    {
        uint CardCode { get; }
        IFullLocationReference LocationReference { get; }
        uint Index { get; }
        byte ReleaseValue { get; }
        ushort CounterAmount { get; }
        ulong Sum { get; }
    }
}