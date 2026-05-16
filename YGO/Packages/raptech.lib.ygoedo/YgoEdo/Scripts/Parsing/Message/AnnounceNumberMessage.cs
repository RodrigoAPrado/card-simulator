using System;
using System.Collections.Generic;
using System.Text;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.System.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class AnnounceNumberMessage : BaseMessage, ISelectionOcgMessage, IAnnounceNumberMessage
    {
        public AnnounceNumberMessage(byte player, List<uint> availableNumbers)
        {
            Player = player;
            AvailableNumbers = availableNumbers;
        }

        public byte Player { get; }
        public IReadOnlyList<uint> AvailableNumbers { get; }
        public override InputType Input => InputType.Value;
        public override int InputCount => 1;

        public override byte[] GetResponse(List<int> input)
        {
            if (input.Count != 1)
                return Array.Empty<byte>();

            var id = input[0];

            if (id < 0 || id >= AvailableNumbers.Count)
                return Array.Empty<byte>();

            return BitConverter.GetBytes(id);
        }

        public bool CanCancel => false;

        public byte[] Cancel()
        {
            return Array.Empty<byte>();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Player={Player}, choose a number below:");
            for (var i = 0; i < AvailableNumbers.Count; i++) sb.AppendLine($"[{i}]=> Declare {AvailableNumbers[i]}");
            return sb.ToString();
        }
    }
}