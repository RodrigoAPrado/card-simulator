using Ygo.Core.Response.Enum;

namespace Ygo.Core.Response
{
    public class CommandResponse
    {
        public bool Ok => ActionState == ActionState.Success;
        public bool Fail => !Ok;
        public ActionState ActionState { get; }
        public CommandResponse(ActionState actionState)
        {
            ActionState = actionState;
        }
    }
}