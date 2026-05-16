using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.System.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Constant;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Util;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class HintEventMessage : BaseHintMessage, IHintEventMessage
    {
        private readonly OCG_GameStrings _text;

        public HintEventMessage(byte player, OCG_GameStrings text) : base($"Player {player}, it is {text}.")
        {
            Player = player;
            _text = text;
            Text = _text.ToSystemStrings();
        }

        public byte Player { get; }
        public SystemStrings Text { get; }
    }
}