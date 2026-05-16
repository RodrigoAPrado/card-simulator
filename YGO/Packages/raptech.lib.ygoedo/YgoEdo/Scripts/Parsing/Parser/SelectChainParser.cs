using System;
using System.Collections.Generic;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Parser.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Util;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Parser
{
    public class SelectChainParser : BaseParser
    {
        protected override IOcgMessage DoParse(byte[] buffer)
        {
            var reader = new PacketReader(buffer);

            reader.ReadByte(); // msg id

            var playerId = reader.ReadByte();
            var cancelable = reader.ReadByte();
            var forced = reader.ReadByte();

            var timingMask = reader.ReadUInt32();
            var timingOtherMask = reader.ReadUInt32();
            var count = reader.ReadUInt32();

            var timingList = new List<OCG_HintTiming>();
            var timingOtherList = new List<OCG_HintTiming>();

            foreach (OCG_HintTiming hintTiming in Enum.GetValues(typeof(OCG_HintTiming)))
            {
                if (((uint)hintTiming & timingMask) != 0) timingList.Add(hintTiming);
                if (((uint)hintTiming & timingOtherMask) != 0) timingOtherList.Add(hintTiming);
            }

            var chains = new List<ChainOption>();

            for (var i = count; i > 0; i--)
            {
                var code = reader.ReadUInt32();
                var controller = reader.ReadByte();
                var location = (OCG_CardLocation)reader.ReadByte();
                var sequence = reader.ReadUInt32();
                var position = (OCG_CardPosition)reader.ReadUInt32();
                var description = reader.ReadULong64();
                reader.Skip(1); // client mode

                chains.Add(new ChainOption(code, new FullLocationReference(controller, location, sequence, position),
                    description));
            }

            return new SelectChainMessage(
                playerId,
                cancelable != 0,
                forced != 0,
                chains,
                timingList,
                timingOtherList
            );
        }
    }
}