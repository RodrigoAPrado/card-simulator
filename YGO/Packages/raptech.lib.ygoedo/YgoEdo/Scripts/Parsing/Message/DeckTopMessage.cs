using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Data.Card;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Util;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class DeckTopMessage : SimpleTextMessage, IDeckTopMessage
    {
        private readonly OCG_CardPosition _position;

        public DeckTopMessage(byte player, uint cardCode, OCG_CardPosition position)
            : base(
                $"Deck Top - Player {player}, card is {CardLibrary.InternalGetCard(cardCode).Name}, position {position}")
        {
            Player = player;
            CardCode = cardCode;
            _position = position;
            Position = _position.ToCardPosition();
        }

        public byte Player { get; }
        public uint CardCode { get; }
        public CardPosition Position { get; }
    }
}