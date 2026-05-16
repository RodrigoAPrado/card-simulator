using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Data.Card;
using YgoSoul.RapTech.Lib.YgoEdo.Util;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component
{
    public class DrawnCard : IDrawnCard
    {
        private readonly OCG_CardPosition _cardPosition;

        public DrawnCard(uint cardCode, OCG_CardPosition cardPosition)
        {
            CardCode = cardCode;
            _cardPosition = cardPosition;
        }

        public uint CardCode { get; }
        public CardPosition CardPosition => _cardPosition.ToCardPosition();

        public override string ToString()
        {
            var card = CardLibrary.InternalGetCard(CardCode);
            return $"{card.Name} that was {_cardPosition}.";
        }
    }
}