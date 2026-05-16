using System;
using System.Collections.Generic;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Constant;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Parser.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Util;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Parser
{
    public class AnnounceRaceAttributeParser : BaseParser
    {
        protected override IOcgMessage DoParse(byte[] buffer)
        {
            var reader = new PacketReader(buffer);
            var message = (OCG_GameMessage)reader.ReadByte();
            var player = reader.ReadByte();
            var count = reader.ReadByte();

            switch (message)
            {
                case OCG_GameMessage.AnnounceRace:
                    return new AnnounceRaceMessage(player, count, GetAvailableRaces(reader.ReadULong64()));
                case OCG_GameMessage.AnnounceAttrib:
                    return new AnnounceAttributeMessage(player, count, GetAvailableAttributes(reader.ReadUInt32()));
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private List<OCG_MonsterRaces> GetAvailableRaces(ulong availableMask)
        {
            var availableList = new List<OCG_MonsterRaces>();

            foreach (OCG_MonsterRaces race in Enum.GetValues(typeof(OCG_MonsterRaces)))
            {
                var raceValue = (ulong)race;

                if (raceValue != 0 && (raceValue & (raceValue - 1)) == 0)
                    if ((availableMask & raceValue) != 0)
                        availableList.Add(race);
            }

            return availableList;
        }

        private List<OCG_MonsterAttributes> GetAvailableAttributes(uint availableMask)
        {
            var availableList = new List<OCG_MonsterAttributes>();

            foreach (OCG_MonsterAttributes attribute in Enum.GetValues(typeof(OCG_MonsterAttributes)))
            {
                var monsterAttribute = (uint)attribute;

                if (monsterAttribute != 0 && (monsterAttribute & (monsterAttribute - 1)) == 0)
                    if ((availableMask & monsterAttribute) != 0)
                        availableList.Add(attribute);
            }

            return availableList;
        }
    }
}