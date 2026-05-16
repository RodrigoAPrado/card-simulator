namespace Ygo.Core.Bridge
{
    public class DuelBridge
    {
        public void Start()
        {
            OcgCoreNative.start_duel();
        }

        public void SendAction(int player, int actionType)
        {
            OcgCoreNative.process(player, actionType);
        }
    }
}