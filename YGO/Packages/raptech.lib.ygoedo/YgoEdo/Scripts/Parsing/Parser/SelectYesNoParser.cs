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
    public class SelectYesNoParser : BaseParser
    {
        protected override IOcgMessage DoParse(byte[] buffer)
        {
            var reader = new PacketReader(buffer);
            var msg = (OCG_GameMessage)reader.ReadByte(); //msg

            switch (msg)
            {
                case OCG_GameMessage.SelectEffectYn:
                    return GetEffectYesNo(reader);
                case OCG_GameMessage.SelectYesNo:
                    return GetYesNo(reader);
                default:
                    throw new ArgumentException();
            }
        }

        private IOcgMessage GetEffectYesNo(PacketReader reader)
        {
            var player = reader.ReadByte();
            var cardCode = reader.ReadUInt32();
            var controller = reader.ReadByte();
            var location = (OCG_CardLocation)reader.ReadByte();
            var sequence = reader.ReadUInt32();
            var position = (OCG_CardPosition)reader.ReadUInt32();
            var description = reader.ReadULong64();
            var card = new CardReference(cardCode, new FullLocationReference(controller, location, sequence, position),
                0);
            return new SelectEffectYesNoMessage(player, card, description);
        }

        private IOcgMessage GetYesNo(PacketReader reader)
        {
            var player = reader.ReadByte();
            var description = reader.ReadULong64();
            return new SelectYesNoMessage(player, description);
        }
    }
}