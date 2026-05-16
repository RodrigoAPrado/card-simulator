using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Data.Card;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class HintCardMessage : BaseHintMessage, IHintCardMessage
    {
        public HintCardMessage(byte player, uint cardCode)
            : base($"Hint: Player={player} Card={CardLibrary.InternalGetCard(cardCode).Name}")
        {
            Player = player;
            CardCode = cardCode;
        }

        public byte Player { get; }
        public uint CardCode { get; }

        public override string ToString()
        {
            return $"Hint: Player={Player} Card={CardLibrary.InternalGetCard(CardCode).Name}";
        }
    }
}