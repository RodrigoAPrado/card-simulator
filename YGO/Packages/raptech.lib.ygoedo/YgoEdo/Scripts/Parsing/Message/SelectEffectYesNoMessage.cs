using System;
using System.Collections.Generic;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.System.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Data.Card;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component;
using YgoSoul.RapTech.Lib.YgoEdo.Util;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class SelectEffectYesNoMessage : ISelectionOcgMessage, ISelectEffectYesNoMessage
    {
        private readonly CardReference _card;
        private readonly ulong _description;

        public SelectEffectYesNoMessage(byte player, CardReference card, ulong description)
        {
            Player = player;
            _card = card;
            _description = description;
            Description = DescriptionUtil.GetDescription(_description, CardLibrary.Instance);
        }

        public byte Player { get; }
        public ICardReference Card => _card;
        public string Description { get; }
        public InputType Input => InputType.Value;
        public int InputCount => 1;

        public byte[] GetResponse(List<int> input)
        {
            if (input.Count != 1)
                return Array.Empty<byte>();

            var id = input[0];

            if (id != 0 && id != 1)
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
            return
                $"\nPlayer {Player}, card effect for card {CardLibrary.InternalGetCard(_card.CardCode).Name}. " +
                $"Description={DescriptionUtil.GetDescription(_description, CardLibrary.Instance)}\n[0] - No\n[1] - Yes";
        }
    }
}