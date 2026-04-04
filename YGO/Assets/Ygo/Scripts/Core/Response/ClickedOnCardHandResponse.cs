namespace Ygo.Core.Response
{
    public class ClickedOnCardHandResponse
    {
        public bool DoNothing { get; }
        public bool NormalSummon { get; set; }
        public bool NormalSet { get; set; }
        public bool TributeSummon { get; set; }
        public bool TributeSet { get; set; }
        public int TributeAmount { get; set; }
        private bool ActivateEffect { get; set; }

        public ClickedOnCardHandResponse(bool doNothing)
        {
            DoNothing = doNothing;
        }
        
        
    }
}