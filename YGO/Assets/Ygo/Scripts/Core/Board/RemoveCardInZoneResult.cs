using Ygo.Core.Abstract;

namespace Ygo.Core.Board
{
    public class RemoveCardFromZoneResult
    {
        public bool Ok => CardRemoved != null;
        public bool Fail => !Ok;
        public ICardInstance CardRemoved { get; }

        public RemoveCardFromZoneResult(ICardInstance cardInstance)
        {
            CardRemoved = cardInstance;
        }
    }
}