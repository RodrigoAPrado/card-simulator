using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.System.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Data.Card;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component;
using YgoSoul.RapTech.Lib.YgoEdo.Util;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class SelectChainMessage : ISelectionOcgMessage, ISelectChainMessage
    {
        private readonly List<ChainOption> _effects;
        private readonly List<OCG_HintTiming> _timing;
        private readonly List<OCG_HintTiming> _timingOther;

        public SelectChainMessage(
            byte player,
            bool cancelable,
            bool forced,
            List<ChainOption> effects,
            List<OCG_HintTiming> timing,
            List<OCG_HintTiming> timingOther
        )
        {
            Player = player;
            CanCancel = cancelable;
            Forced = forced;
            _effects = effects;
            _timing = timing;
            _timingOther = timingOther;
            Timing = _timing.Select(x => x.ToHintTiming()).ToList().AsReadOnly();
            TimingOther = _timingOther.Select(x => x.ToHintTiming()).ToList().AsReadOnly();
        }

        public byte Player { get; }
        public bool Forced { get; }
        public IReadOnlyList<IChainOption> Effects => _effects;
        public IReadOnlyList<HintTiming> Timing { get; }
        public IReadOnlyList<HintTiming> TimingOther { get; }
        public int InputCount => _effects.Count;
        public InputType Input => InputType.SelectChain;

        public bool CanCancel { get; }

        public byte[] GetResponse(List<int> input)
        {
            if (input.Count != 1)
                return Array.Empty<byte>();

            var id = input[0];

            if (id < 0 || id >= _effects.Count)
                return Array.Empty<byte>();
            return BitConverter.GetBytes(id);
        }

        public byte[] Cancel()
        {
            return CanCancel && !Forced ? BitConverter.GetBytes(-1) : Array.Empty<byte>();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine(
                $"Player {Player}, {_effects.Count} effect(s) available. " +
                $"Cancelable: {CanCancel}, Forced: {Forced}."
            );

            sb.AppendLine($"For player {Player}, the timing(s) is(are):");
            foreach (var t in _timing) sb.Append($"{t},");

            sb.AppendLine();
            sb.AppendLine($"For player {1 - Player}, the timing(s) is(are):");
            foreach (var t in _timingOther) sb.Append($"{t},");

            sb.AppendLine();
            if (_effects.Count == 0)
            {
                sb.AppendLine("There is nothing to be activated...");
                return sb.ToString();
            }

            sb.AppendLine("Available chain options:");

            for (var i = 0; i < _effects.Count; i++)
            {
                var e = _effects[i];
                var description = e.Description;
                sb.AppendLine(
                    $"[{i}] => {CardLibrary.InternalGetCard(e.Code).Name}'s effect, Controller={e.LocationReference.Controller}, " +
                    $"Location={e.LocationReference.Location}, Seq={e.LocationReference.Sequence}, Desc={description}"
                );
            }

            return sb.ToString();
        }
    }
}