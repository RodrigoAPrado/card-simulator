using Ygo.Core.Actions.Abstract;

namespace Ygo.Core.Actions
{
    public class CancelAction: IGameAction
    {
        public string ActionName => "Cancel";
        private readonly GameState _gameState;
        
        public CancelAction(GameState gameState)
        {
            _gameState = gameState;
        }
        public void Execute()
        {
            _gameState.CancelAction();
        }
    }
}