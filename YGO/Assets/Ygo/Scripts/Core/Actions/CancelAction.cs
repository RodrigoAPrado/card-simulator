using Ygo.Core.Actions.Abstract;

namespace Ygo.Core.Actions
{
    public class CancelAction : BaseGameAction
    {
        public override string ActionName => "Cancel";
        
        public CancelAction(GameState gameState) : base(gameState)
        {
        }
        
        public override void Execute()
        {
            GameState.CancelAction();
        }
    }
}