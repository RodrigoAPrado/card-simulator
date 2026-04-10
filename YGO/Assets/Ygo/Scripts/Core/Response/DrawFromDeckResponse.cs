using Ygo.Core.Response.Enum;

namespace Ygo.Core.Response
{
    public class DrawFromDeckResponse
    {
        public bool Ok => GameStateResult == GameStateResult.Success;
        public bool Fail => !Ok;
        public GameStateResult GameStateResult { get; }
        public DrawFromDeckResponse(GameStateResult gameStateResult)
        {
            GameStateResult = gameStateResult;
        }
    }
}