using System;
using System.Collections.Generic;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.System.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Data.Card;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Util;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class SelectYesNoMessage : ISelectionOcgMessage, ISelectYesNoMessage
    {
        private readonly ulong _description;

        public SelectYesNoMessage(byte player, ulong description)
        {
            Player = player;
            _description = description;
            Description = DescriptionUtil.GetDescription(_description, CardLibrary.Instance);
        }

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

        public byte Player { get; }
        public string Description { get; }

        public override string ToString()
        {
            return
                $"Player {Player}, activate effect? Description={DescriptionUtil.GetDescription(_description, CardLibrary.Instance)}" +
                $":\n[0] - No\n[1] - Yes";
        }
    }
}