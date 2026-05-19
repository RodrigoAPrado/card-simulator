namespace Ygo.Core.Duel
{
    public class DuelInstance
    {
        private DuelBridge _bridge;
        private DuelState _state;

        public DuelInstance(DuelBridge bridge, DuelState state)
        {
            _bridge = bridge;
            _state = state;
        }
    }
}