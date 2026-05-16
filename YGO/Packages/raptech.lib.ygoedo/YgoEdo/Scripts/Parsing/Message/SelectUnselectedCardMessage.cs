using System;
using System.Collections.Generic;
using System.Text;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.System.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Data.Card;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class SelectUnselectedCardMessage : ISelectionOcgMessage, ISelectUnselectCardMessage
    {
        private readonly List<CardReference> _cardsToSelect;
        private readonly List<CardReference> _cardsToUnselect;

        public SelectUnselectedCardMessage(
            byte player,
            bool finishable,
            bool cancelable,
            uint min,
            uint max,
            List<CardReference> cardsToSelect,
            List<CardReference> cardsToUnselect)
        {
            Player = player;
            Finish = finishable;
            CanCancel = cancelable;
            Min = min;
            Max = max;
            _cardsToSelect = cardsToSelect;
            _cardsToUnselect = cardsToUnselect;
        }

        public InputType Input => InputType.Value;
        public int InputCount => CardsToSelect.Count + CardsToUnselect.Count;

        public byte[] GetResponse(List<int> input)
        {
            if (input.Count != 1)
                return Array.Empty<byte>();

            var id = input[0];

            if (id < 0 && (CanCancel || Finish))
                return BitConverter.GetBytes(-1);
            if (id < 0 || id > CardsToSelect.Count + CardsToUnselect.Count)
                return Array.Empty<byte>();

            var response = new byte[8];
            BitConverter.GetBytes(1).CopyTo(response, 0);
            BitConverter.GetBytes(id).CopyTo(response, 4);
            return response;
        }

        public bool CanCancel { get; }

        public byte[] Cancel()
        {
            return CanCancel ? BitConverter.GetBytes(-1) : Array.Empty<byte>();
        }

        public byte Player { get; }
        public bool Finish { get; }
        public uint Min { get; }
        public uint Max { get; }
        public IReadOnlyList<ICardReference> CardsToSelect => _cardsToSelect;
        public IReadOnlyList<ICardReference> CardsToUnselect => _cardsToUnselect;

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Player {Player}, toggle one of the following cards: ");

            if (CardsToSelect.Count > 0)
            {
                sb.AppendLine("SelectableCards: ");
                foreach (var c in CardsToSelect)
                    sb.AppendLine($"[{c.Index}] => {CardLibrary.InternalGetCard(c.CardCode).Name}");
            }

            if (CardsToUnselect.Count > 0)
            {
                sb.AppendLine("Cards you can unselect:");
                foreach (var c in CardsToUnselect)
                    sb.AppendLine(
                        $"[{c.Index + CardsToUnselect.Count}] => {CardLibrary.InternalGetCard(c.CardCode).Name}");
            }

            return sb.ToString();
        }
    }
}