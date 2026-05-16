using System;
using System.Collections.Generic;
using System.Linq;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Constant;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Parser.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Util;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Parser
{
    public class FieldDisabledParser : BaseParser
    {
        protected override IOcgMessage DoParse(byte[] buffer)
        {
            var reader = new PacketReader(buffer);
            reader.ReadByte(); //msg
            var mask = reader.ReadUInt32();

            var zones = 
                Enum.GetValues(typeof(OCG_Zone))
                .Cast<OCG_Zone>()
                .Where(x => (mask & (uint)x) != 0)
                .ToList();

            return new FieldDisabledMessage(zones, mask);
        }
    }
}