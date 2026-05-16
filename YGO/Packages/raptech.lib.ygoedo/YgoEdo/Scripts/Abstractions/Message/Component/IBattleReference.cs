namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component
{
    public interface IBattleReference
    {
        IFullLocationReference LocationReference { get; }
        uint Atk { get; }
        uint Def { get; }
        bool Destroyed { get; }
    }
}