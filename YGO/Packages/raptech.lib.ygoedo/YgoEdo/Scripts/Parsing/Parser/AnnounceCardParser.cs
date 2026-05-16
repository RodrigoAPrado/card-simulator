using System.Collections.Generic;
using YgoSoul.RapTech.Lib.YgoEdo.Data.Card;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Parser.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Parser.Component;
using YgoSoul.RapTech.Lib.YgoEdo.Util;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Parser
{
    public class AnnounceCardParser : BaseParser
    {
        protected override IOcgMessage DoParse(byte[] buffer)
        {
            var reader = new PacketReader(buffer);
            reader.ReadByte(); //msg
            var player = reader.ReadByte();
            var size = reader.ReadByte();

            var allCards = CardLibrary.AllCards();

            var allOptions = new List<ulong>();
            var availableCards = new List<(string, uint)>();
            var announceCardEvaluator = new AnnounceCardEvaluator();

            for (var i = size; i > 0; i--) allOptions.Add(reader.ReadULong64());

            foreach (var card in allCards)
                if (announceCardEvaluator.IsCardValid(card.Value.Data, allOptions))
                    availableCards.Add((card.Value.Name, card.Value.Data.code));

            return new AnnounceCardMessage(player, availableCards);
        }
    }
}