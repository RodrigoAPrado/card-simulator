using YgoSoul.RapTech.Lib.YgoEdo.Core.Constant;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Parser.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Util;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Parser
{
    public class HintParser : BaseParser
    {
        protected override IOcgMessage DoParse(byte[] buffer)
        {
            var reader = new PacketReader(buffer);
            reader.ReadByte(); //msg
            var hintType = (OCG_GameHintType)reader.ReadByte();
            var player = reader.ReadByte();

            return hintType switch
            {
                OCG_GameHintType.HintEvent => new HintEventMessage(player, (OCG_GameStrings)reader.ReadULong64()),
                OCG_GameHintType.HintSelectMsg => new HintSelectMessage(player, reader.ReadULong64()),
                OCG_GameHintType.HintOpSelected => new HintOpSelectedMessage(player, reader.ReadULong64()),
                OCG_GameHintType.HintCard => new HintCardMessage(player, (uint)reader.ReadULong64()),
                OCG_GameHintType.HintAttrib => new HintAttributeMessage(player,
                    (OCG_MonsterAttributes)reader.ReadUInt32()),
                OCG_GameHintType.HintRace => new HintRaceMessage(player, (OCG_MonsterRaces)reader.ReadULong64()),
                OCG_GameHintType.HintNumber => new HintNumberMessage(player, reader.ReadULong64()),
                OCG_GameHintType.HintCode => new HintCodeMessage(player, (uint)reader.ReadULong64()),
                _ => new UnknownMessage(buffer)
            };
        }
    }
}