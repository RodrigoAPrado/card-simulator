using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class ShuffleDeckMessage : SimpleTextMessage, IShuffleDeckMessage
    {
        public ShuffleDeckMessage(byte player) : base($"Shuffle player {player} deck.")
        {
            Player = player;
        }

        public byte Player { get; }
    }
}