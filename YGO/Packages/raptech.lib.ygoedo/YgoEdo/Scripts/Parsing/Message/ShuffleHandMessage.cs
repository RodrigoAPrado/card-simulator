using System.Collections.Generic;
using System.Text;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Data.Card;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class ShuffleHandMessage : BaseMessage, IShuffleHandMessage
    {
        private readonly List<uint> _cards;

        public ShuffleHandMessage(byte player, List<uint> cards)
        {
            Player = player;
            _cards = cards;
        }

        public byte Player { get; }
        public IReadOnlyList<uint> Cards => _cards;

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"New card order in hand for player {Player}: ");
            foreach (var c in Cards) sb.Append($"{CardLibrary.InternalGetCard(c).Name}, ");
            return sb.ToString().TrimEnd(',');
        }
    }
}