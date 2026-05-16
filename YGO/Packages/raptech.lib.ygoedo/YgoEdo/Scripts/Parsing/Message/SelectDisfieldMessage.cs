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
    public class SelectDisfieldMessage : ISelectionOcgMessage, ISelectDisfieldMessage
    {
        private readonly List<OCG_Zone> _choices;

        public SelectDisfieldMessage(byte player, uint amount, List<OCG_Zone> choices)
        {
            Player = player;
            Amount = amount;
            _choices = choices;
            Choices = _choices.Select(x => x.ToFieldZone()).ToList().AsReadOnly();
        }

        public byte Player { get; }
        public uint Amount { get; }
        public IReadOnlyList<FieldZones> Choices { get; }
        public InputType Input => InputType.Value;
        public int InputCount => _choices.Count;

        public byte[] GetResponse(List<int> input)
        {
            if (input.Count != 1)
                return Array.Empty<byte>();

            var id = input[0];

            if (id < 0 || id >= _choices.Count)
                return Array.Empty<byte>();

            var zone = _choices[id];
            if (!ZoneUtils.ZoneLocation.ContainsKey(zone)
                || !ZoneUtils.ZoneIndexInput.ContainsKey(zone))
                return Array.Empty<byte>();

            var response = new byte[3];
            response[1] = (byte)ZoneUtils.ZoneLocation[zone];
            response[2] = (byte)ZoneUtils.ZoneIndexInput[zone];
            if (ZoneUtils.ZoneIndexQuery.Contains(zone))
                response[0] = Player;
            else
                response[0] = (byte)Math.Abs(Player - 1);

            return response;
        }

        public bool CanCancel => false;

        public byte[] Cancel()
        {
            return Array.Empty<byte>();
        }

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