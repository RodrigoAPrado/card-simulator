namespace Ygo.Core.Actions.Abstract
{
    public interface IGameAction
    {
        string ActionName { get; }
        void Execute();
    }
}