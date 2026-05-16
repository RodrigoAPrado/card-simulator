using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class DamageMessage : BaseMessage, IDamageMessage
    {
        public DamageMessage(byte player, uint damage)
        {
            Player = player;
            Damage = damage;
        }

        public byte Player { get; }
        public uint Damage { get; }

        public override string ToString()
        {
            return $"Player: {Player}, took {Damage} damage!";
        }
    }
}