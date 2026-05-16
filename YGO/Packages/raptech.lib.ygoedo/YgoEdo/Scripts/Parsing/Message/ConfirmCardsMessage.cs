using System.Collections.Generic;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class ConfirmCardsMessage : BaseMessage, IConfirmCardsMessage
    {
        private readonly List<CardReference> _cards;

        public ConfirmCardsMessage(byte player, List<CardReference> cards)
        {
            Player = player;
            _cards = cards;
        }

        public byte Player { get; }
        public IReadOnlyList<ICardReference> Cards => _cards;

        public override string ToString()
        {
            return $"Confirm Cards - Player: {Player}, Cards: {string.Join(", ", _cards)}";
        }
    }
}