using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class NewTurnMessage : SimpleTextMessage, INewTurnMessage
    {
        public NewTurnMessage(byte player) : base($"Your turn, {player}")
        {
            Player = player;
        }

        public byte Player { get; }
    }
}