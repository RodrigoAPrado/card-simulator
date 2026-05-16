using System;
using System.Collections.Generic;
using System.Text;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.System.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Constant;
using YgoSoul.RapTech.Lib.YgoEdo.Data.Card;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component;
using YgoSoul.RapTech.Lib.YgoEdo.Util;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class SelectCounterMessage : ISelectionOcgMessage, ISelectCounterMessage
    {
        private readonly List<CardReference> _cards;
        private readonly OCG_CounterType _counterType;

        public SelectCounterMessage(byte player, OCG_CounterType counterType, ushort counterAmount,
            List<CardReference> cards)
        {
            Player = player;
            _counterType = counterType;
            CounterAmount = counterAmount;
            _cards = cards;
            CounterType = _counterType.ToCounterType();
        }

        public byte Player { get; }
        public CounterType CounterType { get; }
        public ushort CounterAmount { get; }
        public IReadOnlyList<ICardReference> Cards => _cards;
        public InputType Input => InputType.Sort;
        public int InputCount => _cards.Count;
        public bool CanCancel => false;

        public byte[] GetResponse(List<int> ids)
        {
            if (ids.Count != _cards.Count)
                return Array.Empty<byte>();
            var response = new byte[ids.Count * 2];
            var offset = 0;

            for (var i = 0; i < ids.Count; i++)
            {
                if (ids[i] > _cards[i].CounterAmount)
                    return Array.Empty<byte>();

                BitConverter.GetBytes((ushort)ids[i]).CopyTo(response, offset);
                offset += 2;
            }

            return response;
        }

        public byte[] Cancel()
        {
            return Array.Empty<byte>();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Selec counter from cards, you need {(OCG_CounterType)CounterAmount} counters.");
            foreach (var c in _cards)
                sb.AppendLine($"{CardLibrary.InternalGetCard(c.CardCode).Name} has {c.CounterAmount} counters...");

            return sb.ToString();
        }
    }
}