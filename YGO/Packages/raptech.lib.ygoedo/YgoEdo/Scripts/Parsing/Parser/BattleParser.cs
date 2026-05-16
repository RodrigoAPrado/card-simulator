using YgoSoul.RapTech.Lib.YgoEdo.Core.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Parser.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Util;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Parser
{
    public class BattleParser : BaseParser
    {
        protected override IOcgMessage DoParse(byte[] buffer)
        {
            var reader = new PacketReader(buffer);
            reader.ReadByte(); //msg
            var fullAttackerLocation = new FullLocationReference(
                reader.ReadByte(),
                (OCG_CardLocation)reader.ReadByte(),
                reader.ReadUInt32(),
                (OCG_CardPosition)reader.ReadUInt32());
            var attackerAttack = reader.ReadUInt32();
            var attackerDefense = reader.ReadUInt32();
            var attackerDestroyed = reader.ReadByte() == 1;
            var fullDefenserLocation = new FullLocationReference(
                reader.ReadByte(),
                (OCG_CardLocation)reader.ReadByte(),
                reader.ReadUInt32(),
                (OCG_CardPosition)reader.ReadUInt32());
            var defenderAttack = reader.ReadUInt32();
            var defenderDefense = reader.ReadUInt32();
            var defenderDestroyed = reader.ReadByte() == 1;

            return new BattleMessage(
                new BattleReference(
                    fullAttackerLocation,
                    attackerAttack,
                    attackerDefense,
                    attackerDestroyed),
                new BattleReference(
                    fullDefenserLocation,
                    defenderAttack,
                    defenderDefense,
                    defenderDestroyed));
        }
    }
}