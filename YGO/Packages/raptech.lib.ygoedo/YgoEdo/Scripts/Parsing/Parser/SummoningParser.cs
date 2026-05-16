using System;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Constant;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Parser.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Util;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Parser
{
    public class SummoningParser : BaseParser
    {
        protected override IOcgMessage DoParse(byte[] buffer)
        {
            var reader = new PacketReader(buffer);
            var messageType = (OCG_GameMessage)reader.ReadByte(); //msg
            var cardCode = reader.ReadUInt32();

            var player = reader.ReadByte();
            var location = (OCG_CardLocation)reader.ReadByte();
            var sequence = reader.ReadUInt32();
            var position = (OCG_CardPosition)reader.ReadUInt32();

            var card = new CardReference(cardCode, new FullLocationReference(player, location, sequence, position), 0);

            switch (messageType)
            {
                case OCG_GameMessage.Summoning:
                    return new SummoningMessage(card);
                case OCG_GameMessage.Set:
                    return new SetMessage(card);
                case OCG_GameMessage.SpSummoning:
                    return new SpecialSummoningMessage(card);
                case OCG_GameMessage.FlipSummoning:
                    return new FlipSummoningMessage(card);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}