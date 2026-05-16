using System;
using System.Collections.Generic;
using System.Text;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.System.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class AnnounceCardMessage : BaseMessage, ISelectionOcgMessage, IAnnounceCardMessage
    {
        public AnnounceCardMessage(byte player, List<(string, uint)> availableCards)
        {
            Player = player;
            AvailableCards = availableCards;
        }

        public byte Player { get; }
        public IReadOnlyList<(string, uint)> AvailableCards { get; }

        public List<(int, string)> Query(string value)
        {
            var list = new List<(int, string)>();
            value = value.ToLower();

            for (var i = 0; i < AvailableCards.Count; i++)
            {
                var card = AvailableCards[i];
                var cardName = card.Item1.ToLower();
                if (cardName.StartsWith(value)
                    || cardName.Contains(value)
                    || Compute(value, cardName) < 1)
                    list.Add((i, $"{card.Item1} - {card.Item2}"));
            }

            return list;
        }

        public override InputType Input => InputType.AnnounceCard;
        public override int InputCount => 1;

        public override byte[] GetResponse(List<int> input)
        {
            if (input.Count != 1)
                return Array.Empty<byte>();

            var id = input[0];

            if (id >= AvailableCards.Count || id < 0)
                return Array.Empty<byte>();

            return BitConverter.GetBytes(AvailableCards[id].Item2);
        }

        public bool CanCancel => false;

        public byte[] Cancel()
        {
            return Array.Empty<byte>();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Select one of the following cards (you may type the card's name to query):");
            uint index = 0;
            foreach (var card in AvailableCards)
            {
                sb.AppendLine($"[{index}] => {card.Item1} - {card.Item2}");
                index++;
            }

            return sb.ToString();
        }

        private static int Compute(string s, string t)
        {
            var n = s.Length;
            var m = t.Length;
            var d = new int[n + 1, m + 1];

            if (n == 0) return m;
            if (m == 0) return n;

            for (var i = 0; i <= n; d[i, 0] = i++) ;
            for (var j = 0; j <= m; d[0, j] = j++) ;

            for (var i = 1; i <= n; i++)
            for (var j = 1; j <= m; j++)
            {
                var cost = t[j - 1] == s[i - 1] ? 0 : 1;

                d[i, j] = Math.Min(
                    Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                    d[i - 1, j - 1] + cost);
            }

            return d[n, m];
        }
    }
}