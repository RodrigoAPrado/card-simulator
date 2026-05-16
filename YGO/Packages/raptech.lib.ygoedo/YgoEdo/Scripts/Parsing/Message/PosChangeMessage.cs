using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Data.Card;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Util;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class PosChangeMessage : BaseMessage, IPosChangeMessage
    {
        private readonly OCG_CardLocation _currentLocation;
        private readonly OCG_CardPosition _currentPosition;
        private readonly OCG_CardPosition _previousPosition;

        public PosChangeMessage(
            uint cardCode,
            byte currentController,
            OCG_CardLocation currentLocation,
            byte currentSequence,
            OCG_CardPosition previousPosition,
            OCG_CardPosition currentPosition
        )
        {
            CardCode = cardCode;
            CurrentController = currentController;
            _currentLocation = currentLocation;
            CurrentSequence = currentSequence;
            _previousPosition = previousPosition;
            _currentPosition = currentPosition;
            CurrentPosition = _currentPosition.ToCardPosition();
            CurrentLocation = _currentLocation.ToLocation();
            PreviousPosition = _previousPosition.ToCardPosition();
        }

        public uint CardCode { get; }
        public byte CurrentController { get; }
        public Location CurrentLocation { get; }
        public byte CurrentSequence { get; }
        public CardPosition PreviousPosition { get; }
        public CardPosition CurrentPosition { get; }

        public override string ToString()
        {
            return
                $"Card {CardLibrary.InternalGetCard(CardCode).Name} was changed from {_previousPosition} to {_currentPosition}";
        }
    }
}