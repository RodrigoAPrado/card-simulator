using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Data.Card;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class MatchKillMessage : BaseMessage, IMatchKillMessage
    {
        public MatchKillMessage(uint cardCode)
        {
            CardCode = cardCode;
        }

        public uint CardCode { get; }

        public override string ToString()
        {
            return $"MatchKill, Card={CardLibrary.InternalGetCard(CardCode).Name}";
        }
    }
}