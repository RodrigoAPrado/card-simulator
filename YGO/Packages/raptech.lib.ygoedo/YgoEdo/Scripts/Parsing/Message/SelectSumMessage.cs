using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.System.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class SelectSumMessage : ISelectionOcgMessage, ISelectSumMessage
    {
        private readonly List<CardReference> _canSelect;
        private readonly List<CardReference> _mustSelect;

        public SelectSumMessage(
            byte player,
            bool hasMaximumChoices,
            uint targetSum,
            uint minimumChoices,
            uint maximumChoices,
            List<CardReference> mustSelect,
            List<CardReference> canSelect
        )
        {
            Player = player;
            HasMaximumChoices = hasMaximumChoices;
            TargetSum = targetSum;
            MinimumChoices = minimumChoices;
            MaximumChoices = maximumChoices;
            _mustSelect = mustSelect;
            _canSelect = canSelect;
        }

        public InputType Input => InputType.Selections;
        public int InputCount => MustSelect.Count + CanSelect.Count;
        public bool CanCancel => false;

        public byte[] GetResponse(List<int> ids)
        {
            var invalid = ids.Any(x => x >= MustSelect.Count + CanSelect.Count || x < 0);

            if (invalid)
                return Array.Empty<byte>();

            if (MustSelect.Count > 0)
                foreach (var must in MustSelect)
                    if (!ids.Contains((int)must.Index))
                        return Array.Empty<byte>();

            if (HasMaximumChoices && ids.Count > MaximumChoices && ids.Count < MinimumChoices)
                return Array.Empty<byte>();

            var selectedCards = new List<CardReference>();

            foreach (var id in ids)
            {
                if (id < MustSelect.Count)
                {
                    selectedCards.Add(_mustSelect[id]);
                    continue;
                }

                selectedCards.Add(_canSelect[id - _mustSelect.Count]);
            }

            var tot = (uint)selectedCards.Sum(x => (int)x.Sum);
            if (tot < TargetSum)
                return Array.Empty<byte>();

            var response = new byte[8 + ids.Count * 4];
            var offset = 4;

            BitConverter.GetBytes(ids.Count).CopyTo(response, offset);
            offset += 4;

            foreach (var i in ids)
            {
                BitConverter.GetBytes(i).CopyTo(response, offset);
                offset += 4;
            }

            return response;
        }

        public byte[] Cancel()
        {
            return Array.Empty<byte>();
        }

        public byte Player { get; }
        public bool HasMaximumChoices { get; }
        public uint TargetSum { get; }
        public uint MinimumChoices { get; }
        public uint MaximumChoices { get; }
        public IReadOnlyList<ICardReference> MustSelect => _mustSelect;
        public IReadOnlyList<ICardReference> CanSelect => _canSelect;

        public override string ToString()
        {
            var max = "";
            if (HasMaximumChoices) max = $", and you must select between {MinimumChoices} and {MaximumChoices} cards";
            var sb = new StringBuilder();
            sb.AppendLine($"You need a sum of {TargetSum}{max}.");
            if (MustSelect.Count > 0)
                sb.AppendLine("You must select the following cards:");
            foreach (var card in MustSelect) sb.AppendLine($"[{card.Index}] => {card}, SumValue={card.Sum}");
            sb.AppendLine("You can select the following cards:");
            foreach (var card in CanSelect) sb.AppendLine($"[{card.Index}] => {card}, SumValue={card.Sum}");

            return sb.ToString();
        }
    }
}