using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.System.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Data.Card;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Util;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class SelectPositionMessage : ISelectionOcgMessage, ISelectPositionMessage
    {
        private readonly List<OCG_CardPosition> _positionAvailable;

        public SelectPositionMessage(byte player, uint cardCode, List<OCG_CardPosition> positionAvailable)
        {
            Player = player;
            CardCode = cardCode;
            _positionAvailable = positionAvailable;
            PositionAvailable = _positionAvailable.Select(x => x.ToCardPosition()).ToList().AsReadOnly();
        }

        public InputType Input => InputType.Value;
        public int InputCount => PositionAvailable.Count;

        public byte[] GetResponse(List<int> input)
        {
            if (input.Count != 1)
                return Array.Empty<byte>();

            var id = input[0];

            if (id < 0 || id >= PositionAvailable.Count)
                return Array.Empty<byte>();
            var position = (uint)PositionAvailable[id];
            return BitConverter.GetBytes(position);
        }

        public bool CanCancel => false;

        public byte[] Cancel()
        {
            return Array.Empty<byte>();
        }

        public byte Player { get; }
        public uint CardCode { get; }
        public IReadOnlyList<CardPosition> PositionAvailable { get; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Player {Player} select a position for card {CardLibrary.InternalGetCard(CardCode).Name}:");
            for (var i = 0; i < PositionAvailable.Count; i++) sb.AppendLine($"[{i}] => {PositionAvailable[i]}");

            return sb.ToString();
        }
    }
}