namespace Ygo.Core.Actions.Abstract
{
    public abstract class BaseGameAction : IGameAction
    {
        public abstract string ActionName { get; }
        protected readonly GameState GameState;

        protected BaseGameAction(GameState gameState)
        {
            GameState = gameState;
        }
        
        public abstract void Execute();
    }
}