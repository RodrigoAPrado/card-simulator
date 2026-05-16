using System.Collections.Generic;
using System.Text;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class DrawMessage : BaseMessage, IDrawMessage
    {
        private readonly List<DrawnCard> _cards;

        public DrawMessage(uint player, List<DrawnCard> cards)
        {
            Player = player;
            _cards = cards;
        }

        public uint Player { get; }
        public IReadOnlyList<IDrawnCard> Cards => _cards;

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Player {Player} drew the following cards:");
            foreach (var card in _cards) sb.AppendLine($"- {card.ToString()}");

            return sb.ToString();
        }
    }
}