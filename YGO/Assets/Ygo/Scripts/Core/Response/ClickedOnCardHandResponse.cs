using Ygo.Core.Abstract;

namespace Ygo.Core.Response
{
    public class ClickedOnCardHandResponse
    {
        public bool DoNothing => Card != null;
        public bool NormalSummon { get; set; }
        public bool NormalSet { get; set; }
        public bool TributeSummon { get; set; }
        public bool TributeSet { get; set; }
        public int TributeAmount { get; set; }
        private bool ActivateEffect { get; set; }
        public ICardInstance Card { get; }

        public ClickedOnCardHandResponse(ICardInstance card)
        {
            Card = card;
        }
    }
}