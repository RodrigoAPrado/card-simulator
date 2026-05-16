using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Parser.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Util;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Parser
{
    public class SwapGraveDeckParser : BaseParser
    {
        protected override IOcgMessage DoParse(byte[] buffer)
        {
            var reader = new PacketReader(buffer);
            reader.ReadByte(); //msg
            var player = reader.ReadByte();
            var extraSize = reader.ReadUInt32();
            var dataSize = reader.ReadUInt32();
            var data = new byte[dataSize];

            for (var i = 0; i < dataSize; i++) data[i] = reader.ReadByte();

            return new SwapGraveDeckMessage(player, extraSize, data);
        }
    }
}