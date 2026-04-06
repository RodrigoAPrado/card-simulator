using System;
using Ygo.Core.Abstract;
using Ygo.Core.Board.Abstract;

namespace Ygo.Core.Board
{
    public class BoardZone : IBoardZone
    {
        public Guid Id { get; }
        public ZoneType Type { get; }
        public ZonePosition Position { get; }
        public ICardInstance CardInZone { get; private set; }
        public bool IsFree => CardInZone == null;
        private readonly IPutCardInZoneValidator _validator;

        public BoardZone(ZoneType type, ZonePosition position, IPutCardInZoneValidator validator)
        {
            Id = new Guid();
            Type = type;
            Position = position;
            _validator = validator;
        }

        public bool CanPutCard(ICardInstance cardInstance) => IsFree && _validator.Validate(cardInstance);

        public PutCardInZoneResult TryPutCard(ICardInstance cardInstance)
        {
            if (!IsFree)
                return new PutCardInZoneResult(PutCardInZoneResultCode.ZoneOccupied);

            if (!_validator.Validate(cardInstance))
                return new PutCardInZoneResult(PutCardInZoneResultCode.ZoneOccupied);
            
            CardInZone = cardInstance;
            return new PutCardInZoneResult(PutCardInZoneResultCode.Success);
        }

        public RemoveCardFromZoneResult TryRemoveCard()
        {
            var response = new RemoveCardFromZoneResult(CardInZone);
            CardInZone = null;
            return response;
        }
    }
}