using Ygo.Core.Abstract;

namespace Ygo.Core.Response
{
    public class ClickedOnCardResponse
    {
        public bool DoNothing => Card == null;
        public bool NormalSummon { get; set; }
        public bool NormalSet { get; set; }
        public bool TributeSummon { get; set; }
        public bool TributeSet { get; set; }
        public int TributeAmount { get; set; }
        public bool ActivateEffect { get; set; }
        public bool Attack { get; set; }
        public ICardInstance Card { get; }

        public ClickedOnCardResponse(ICardInstance card)
        {
            Card = card;
        }
    }
}