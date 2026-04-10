using Ygo.Core.Response.Enum;

namespace Ygo.Core.Response
{
    public class CommandResponse
    {
        public bool Ok => GameStateResult == GameStateResult.Success;
        public bool Fail => !Ok;
        public GameStateResult GameStateResult { get; }
        public CommandResponse(GameStateResult gameStateResult)
        {
            GameStateResult = gameStateResult;
        }
    }
}