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
    public class SelectTributeMessage : ISelectionOcgMessage, ISelectTributeMessage
    {
        private readonly List<CardReference> _cards;

        public SelectTributeMessage(byte player, bool cancelable, uint min, uint max, List<CardReference> cards)
        {
            Player = player;
            CanCancel = cancelable;
            Min = min;
            Max = max;
            _cards = cards;
        }

        public InputType Input => InputType.Selections;
        public int InputCount => Cards.Count();
        public bool CanCancel { get; }

        public byte[] GetResponse(List<int> ids)
        {
            var invalid = ids.Any(x => x >= Cards.Count || x < 0);

            if (invalid)
                return Array.Empty<byte>();

            var value = ids.Sum(x => Cards[x].ReleaseValue);

            if (value < Min)
                return Cancel();
            if (ids.Count > Max)
                return Cancel();

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
            return CanCancel ? BitConverter.GetBytes(-1) : Array.Empty<byte>();
        }

        public byte Player { get; }
        public uint Min { get; }
        public uint Max { get; }
        public IReadOnlyList<ICardReference> Cards => _cards;


        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Select at most {Max} cards, but release value must be above {Min}:");
            foreach (var c in Cards)
                sb.AppendLine(
                    $"{c.Index} => {CardLibrary.InternalGetCard(c.CardCode).Name}, Value: {c.ReleaseValue}...");

            if (CanCancel)
                sb.AppendLine("Input \"Cancel\" if you want to cancel this selection...");
            else
                sb.AppendLine("You cannot cancel this selection.");

            sb.AppendLine("Input \"Enter\" to finish your selection.");
            return sb.ToString();
        }
    }
}