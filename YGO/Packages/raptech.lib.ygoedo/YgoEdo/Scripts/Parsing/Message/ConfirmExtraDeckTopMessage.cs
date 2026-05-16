using System.Collections.Generic;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class ConfirmExtraDeckTopMessage : BaseMessage, IConfirmExtraDeckTopMessage
    {
        private readonly List<CardReference> _cards;

        public ConfirmExtraDeckTopMessage(byte player, List<CardReference> cards)
        {
            Player = player;
            _cards = cards;
        }

        public byte Player { get; }
        public IReadOnlyList<ICardReference> Cards => _cards;

        public override string ToString()
        {
            return $"Confirm ExtraTopDeck - Player: {Player}, Cards: {string.Join(", ", _cards)}";
        }
    }
}