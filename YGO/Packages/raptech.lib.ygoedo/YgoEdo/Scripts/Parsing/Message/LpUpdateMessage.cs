using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class LpUpdateMessage : BaseMessage, ILpUpdateMessage
    {
        public LpUpdateMessage(byte player, uint lp)
        {
            Player = player;
            Lp = lp;
        }

        public byte Player { get; }
        public uint Lp { get; }

        public override string ToString()
        {
            return $"LP update, Player={Player}, LP={Lp}";
        }
    }
}