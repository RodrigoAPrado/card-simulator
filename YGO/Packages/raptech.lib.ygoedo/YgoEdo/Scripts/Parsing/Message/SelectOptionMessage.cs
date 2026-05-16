using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.System.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Data.Card;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Util;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class SelectOptionMessage : ISelectionOcgMessage, ISelectOptionMessage
    {
        private readonly List<ulong> _options;

        public SelectOptionMessage(byte player, List<ulong> options)
        {
            Player = player;
            _options = options;
            Options = _options.Select(x => DescriptionUtil.GetDescription(x, CardLibrary.Instance)).ToList()
                .AsReadOnly();
        }

        public InputType Input => InputType.Value;
        public int InputCount => Options.Count;

        public byte[] GetResponse(List<int> input)
        {
            if (input.Count != 1)
                return Array.Empty<byte>();

            var id = input[0];

            if (id < 0 || id >= Options.Count)
                return Array.Empty<byte>();

            return BitConverter.GetBytes(id);
        }

        public bool CanCancel => false;

        public byte[] Cancel()
        {
            return Array.Empty<byte>();
        }

        public byte Player { get; }
        public IReadOnlyList<string> Options { get; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Player: {Player}, here are your options:");

            for (var i = 0; i < Options.Count; i++)
                sb.AppendLine($"[{i}] => {DescriptionUtil.GetDescription(_options[i], CardLibrary.Instance)}, {_options[i]:x16}");
            return sb.ToString();
        }
    }
}