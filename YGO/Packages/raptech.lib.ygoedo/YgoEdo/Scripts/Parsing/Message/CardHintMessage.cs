using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Constant;
using YgoSoul.RapTech.Lib.YgoEdo.Data.Card;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component;
using YgoSoul.RapTech.Lib.YgoEdo.Util;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class CardHintMessage : BaseMessage, ICardHintMessage
    {
        private readonly OCG_CardHint _cardHint;
        private readonly ulong _description;

        private readonly FullLocationReference _locationReference;

        public CardHintMessage(FullLocationReference fullLocationReference, OCG_CardHint cardHint, ulong description)
        {
            _locationReference = fullLocationReference;
            _cardHint = cardHint;
            _description = description;
            CardHint = _cardHint.ToDuelCardHint();
            Description = DescriptionUtil.GetDescription(_description, _cardHint, CardLibrary.Instance);
        }

        public IFullLocationReference LocationReference => _locationReference;
        public DuelCardHint CardHint { get; }
        public string Description { get; }

        public override string ToString()
        {
            return
                $"{_locationReference}. Hint={_cardHint}, Description={DescriptionUtil.GetDescription(_description, _cardHint, CardLibrary.Instance)}, DescriptionDex={_description:x16}";
        }
    }
}