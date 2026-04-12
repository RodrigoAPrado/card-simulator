using Ygo.Core.Actions.Abstract;

namespace Ygo.Core.Actions
{
    public class EmptyAction : IGameAction
    {
        public string ActionName => "";
        public void Execute()
        {
        }
    }
}