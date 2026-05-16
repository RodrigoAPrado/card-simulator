using System;
using System.Linq;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Constant;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Parser.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Util;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Parser
{
    public class MoveParser : BaseParser
    {
        protected override IOcgMessage DoParse(byte[] buffer)
        {
            var reader = new PacketReader(buffer);

            reader.ReadByte(); // MSG_MOVE

            var cardCode = reader.ReadUInt32();

            var prevController = reader.ReadByte();
            var prevLocation = (OCG_CardLocation)reader.ReadByte();
            var prevSequence = reader.ReadUInt32();
            var prevPosition = (OCG_CardPosition)reader.ReadUInt32();

            var newController = reader.ReadByte();
            var newLocation = (OCG_CardLocation)reader.ReadByte();
            var newSequence = reader.ReadUInt32();
            var newPosition = (OCG_CardPosition)reader.ReadUInt32();

            var mask = reader.ReadUInt32();

            var reasons = 
                Enum.GetValues(typeof(OCG_Reason))
                .Cast<OCG_Reason>()
                .Where(x => x != OCG_Reason.None)
                .Where(x => (mask & (uint)x) != 0)
                .ToList();

            return new MoveMessage(
                cardCode,
                new FullLocationReference(prevController, prevLocation, prevSequence, prevPosition),
                new FullLocationReference(newController, newLocation, newSequence, newPosition),
                reasons
            );
        }
    }
}