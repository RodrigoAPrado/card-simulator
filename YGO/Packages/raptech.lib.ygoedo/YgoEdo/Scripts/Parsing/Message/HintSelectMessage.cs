using System;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Constant;
using YgoSoul.RapTech.Lib.YgoEdo.Data.Card;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class HintSelectMessage : BaseHintMessage, IHintSelectMessage
    {
        private readonly ulong _description;

        public HintSelectMessage(byte player, ulong description) : base(
            $"Player {player}, {GetHintText(description)}...")
        {
            Player = player;
            _description = description;
            Description = GetHintText(_description);
        }

        public byte Player { get; }
        public string Description { get; }


        private static string GetHintText(ulong hint)
        {
            var rawBits = BitConverter.GetBytes(hint);
            var cardText = BitConverter.ToUInt16(rawBits, 0);
            var cardIdRaw = BitConverter.ToUInt32(rawBits, 2);
            var cardId = cardIdRaw >> 4;

            if (CardLibrary.Instance.HasCard(cardId)) return CardLibrary.InternalGetCard(cardId).Strings[cardText];

            return Enum.IsDefined(typeof(OCG_GameStrings), hint)
                ? ((OCG_GameStrings)hint).ToString()
                : CardLibrary.InternalGetCard((uint)hint).Name;
        }
    }
}