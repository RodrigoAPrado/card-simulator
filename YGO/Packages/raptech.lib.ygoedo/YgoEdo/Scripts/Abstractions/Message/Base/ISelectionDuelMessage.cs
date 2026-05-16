namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base
{
    public interface ISelectionDuelMessage : IDuelMessage
    {
        bool CanCancel { get; }
    }
}