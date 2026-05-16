using System.Collections.Generic;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Parser.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Util;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Parser
{
    public class RemoveCardsParser : BaseParser
    {
        protected override IOcgMessage DoParse(byte[] buffer)
        {
            var reader = new PacketReader(buffer);
            reader.ReadByte(); //msg
            var size = reader.ReadUInt32();
            var list = new List<FullLocationReference>();
            for (var i = size; i > 0; i--)
                list.Add(new FullLocationReference(
                    reader.ReadByte(),
                    (OCG_CardLocation)reader.ReadByte(),
                    reader.ReadUInt32(),
                    (OCG_CardPosition)reader.ReadUInt32()));

            return new RemoveCardsMessage(list);
        }
    }
}