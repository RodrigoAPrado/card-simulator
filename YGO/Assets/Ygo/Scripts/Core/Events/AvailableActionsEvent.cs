using Ygo.Core.Events.Abstract;
using Ygo.Core.Response;

namespace Ygo.Core.Events
{
    public class AvailableActionsEvent : IGameEvent
    {
        public ActionQuery Actions { get; }
        public AvailableActionsEvent(ActionQuery actions)
        {
            Actions = actions;
        }
    }
}