using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class SwapMessage : BaseMessage, ISwapMessage
    {
        private readonly CardReference _card1;
        private readonly CardReference _card2;

        public SwapMessage(CardReference card1, CardReference card2)
        {
            _card1 = card1;
            _card2 = card2;
        }

        public ICardReference Card1 => _card1;
        public ICardReference Card2 => _card2;

        public override string ToString()
        {
            return $"Card 1={Card1} swap with Card 2={Card2}";
        }
    }
}