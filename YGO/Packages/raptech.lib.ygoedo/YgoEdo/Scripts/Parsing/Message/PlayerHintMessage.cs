using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Constant;
using YgoSoul.RapTech.Lib.YgoEdo.Data.Card;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Util;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class PlayerHintMessage : BaseMessage, IPlayerHintMessage
    {
        private readonly ulong _description;
        private readonly OCG_PlayerHint _hint;

        public PlayerHintMessage(byte player, OCG_PlayerHint hint, ulong description)
        {
            Player = player;
            _hint = hint;
            _description = description;
            Hint = hint.ToPlayerHint();
            Description = DescriptionUtil.GetDescription(_description, CardLibrary.Instance);
        }

        public byte Player { get; }
        public PlayerHint Hint { get; }
        public string Description { get; }

        public override string ToString()
        {
            return $"Player={Player}, Hint={_hint}, Description={DescriptionUtil.GetDescription(_description, CardLibrary.Instance)}";
        }
    }
}