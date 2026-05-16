using System.Collections.Generic;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class ConfirmDeckTopMessage : BaseMessage, IConfirmDeckTopMessage
    {
        private readonly List<CardReference> _cards;

        public ConfirmDeckTopMessage(byte player, List<CardReference> cards)
        {
            Player = player;
            _cards = cards;
        }

        public byte Player { get; }
        public IReadOnlyList<ICardReference> Cards => _cards;

        public override string ToString()
        {
            return $"Confirm TopDeck - Player: {Player}, Cards: {string.Join(", ", _cards)}";
        }
    }
}