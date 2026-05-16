namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component
{
    public interface IChainOption
    {
        uint Code { get; }
        string Description { get; }
        IFullLocationReference LocationReference { get; }
    }
}