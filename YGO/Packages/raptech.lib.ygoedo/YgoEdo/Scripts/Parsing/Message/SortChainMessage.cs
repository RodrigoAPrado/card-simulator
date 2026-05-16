using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.System.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Data.Card;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class SortChainMessage : ISelectionOcgMessage, ISortChainMessage
    {
        private readonly List<CardReference> _cards;

        public SortChainMessage(byte player, List<CardReference> cards)
        {
            Player = player;
            _cards = cards;
        }

        public InputType Input => InputType.Sort;
        public int InputCount => Cards.Count;
        public bool CanCancel => true;

        public byte[] GetResponse(List<int> ids)
        {
            var invalid = ids.Any(x => x >= Cards.Count || x < 0);

            if (invalid)
                return Array.Empty<byte>();

            if (ids.Count != Cards.Count)
                return Array.Empty<byte>();

            var response = new byte[ids.Count];
            for (var i = 0; i < ids.Count; i++) response[i] = (byte)ids[i];

            return response;
        }

        public byte[] Cancel()
        {
            return new[] {unchecked((byte)-1)};
        }

        public byte Player { get; }
        public IReadOnlyList<ICardReference> Cards => _cards;

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(
                $"Player {Player}, reorder the chain inputing their index with commas like a,b,c, or \"Cancel\":");
            foreach (var c in Cards) sb.AppendLine($"{CardLibrary.InternalGetCard(c.CardCode).Name}");

            return sb.ToString();
        }
    }
}