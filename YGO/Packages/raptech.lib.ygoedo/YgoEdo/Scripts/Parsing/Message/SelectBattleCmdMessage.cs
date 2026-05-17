using System;
using System.Collections.Generic;
using System.Text;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component.Command.Base;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.System.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component.Command.Base;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class SelectBattleCmdMessage : ISelectionOcgMessage, ISelectBattleCommandMessage
    {
        private readonly List<BattleCmdChoice> _choices;

        public SelectBattleCmdMessage(byte player, List<BattleCmdChoice> choices)
        {
            Player = player;
            _choices = choices;
        }

        public InputType Input => InputType.Value;
        public int InputCount => _choices.Count;

        public byte[] GetResponse(List<int> input)
        {
            if (input.Count != 1)
                return Array.Empty<byte>();

            var id = input[0];

            if (id >= _choices.Count)
                return Array.Empty<byte>();

            var choice = _choices[id];

            var value = (choice.Index << 16) | (uint)choice.Action;

            return BitConverter.GetBytes(value);
        }

        public byte Player { get; }
        public IReadOnlyList<IBattleCommand> Choices => _choices;

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Player {Player}, input your action:");
            for (var i = 0; i < _choices.Count; i++) sb.AppendLine($"[{i}] => {_choices[i]}");
            return sb.ToString();
        }

        public bool CanCancel => false;
        public byte[] Cancel()
        {
            return Array.Empty<byte>();
        }
    }
}