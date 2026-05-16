using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component;
using YgoSoul.RapTech.Lib.YgoEdo.Data.Card;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class FlipSummoningMessage : BaseMessage, IFlipSummoningMessage
    {
        private readonly CardReference _card;

        public FlipSummoningMessage(CardReference card)
        {
            _card = card;
        }

        public ICardReference Card => _card;

        public override string ToString()
        {
            return $"{CardLibrary.InternalGetCard(Card.CardCode).Name} is being flip summoned for " +
                   $"{Card.LocationReference.Controller} on " +
                   $"{Card.LocationReference.Location} in sequence " +
                   $"{Card.LocationReference.Sequence} and position " +
                   $"{Card.LocationReference.Position}";
        }
    }
}