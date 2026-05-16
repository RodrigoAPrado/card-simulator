using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.System.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Constant;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Util;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class SelectPlaceMessage : ISelectionOcgMessage, ISelectPlaceMessage
    {
        private readonly List<OCG_Zone> _choices;

        public SelectPlaceMessage(byte player, uint amount, List<OCG_Zone> choices)
        {
            Player = player;
            Amount = amount;
            _choices = choices;
            Choices = _choices.Select(x => x.ToFieldZone()).ToList().AsReadOnly();
        }

        public InputType Input => InputType.Value;
        public int InputCount => _choices.Count;

        public byte[] GetResponse(List<int> input)
        {
            if (input.Count <= 0 || input.Count >= _choices.Count)
                return Array.Empty<byte>();

            if (input.Count > Amount)
                return Array.Empty<byte>();

            var fullResponse = new List<byte>();

            foreach (var value in input)
            {
                var zone = _choices[value];

                if (!ZoneUtils.ZoneLocation.ContainsKey(zone)
                    || !ZoneUtils.ZoneIndexInput.ContainsKey(zone))
                    return Array.Empty<byte>();

                var response = new List<byte>();

                if (ZoneUtils.ZoneIndexQuery.Contains(zone))
                    response.Add(Player);
                else
                    response.Add((byte)Math.Abs(Player - 1));

                response.Add((byte)ZoneUtils.ZoneLocation[zone]);
                response.Add((byte)ZoneUtils.ZoneIndexInput[zone]);

                fullResponse.AddRange(response);
            }

            return fullResponse.ToArray();
        }

        public bool CanCancel => false;

        public byte[] Cancel()
        {
            return Array.Empty<byte>();
        }

        public byte Player { get; }
        public uint Amount { get; }
        public IReadOnlyList<FieldZones> Choices { get; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Player {Player}, you need to select {Amount} places.");
            sb.AppendLine("Please input your action:");
            for (var i = 0; i < _choices.Count; i++) sb.AppendLine($"{i} -> Place card on {_choices[i]}");
            return sb.ToString();
        }
    }
}