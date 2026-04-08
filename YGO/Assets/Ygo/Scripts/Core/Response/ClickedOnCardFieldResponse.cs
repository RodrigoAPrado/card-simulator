using Ygo.Core.Abstract;

namespace Ygo.Core.Response
{
    public class ClickedOnCardFieldResponse
    {
        public bool DoNothing => Card == null;
        public bool Attack { get; set; }
        public ICardInstance Card { get; }

        public ClickedOnCardFieldResponse(ICardInstance card)
        {
            Card = card;
        }
    }
}