using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Data.Card;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class HintCodeMessage : BaseMessage, IHintCodeMessage
    {
        public HintCodeMessage(byte player, uint cardCode)
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