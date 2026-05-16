using System.Collections.Generic;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Struct;

namespace YgoSoul.RapTech.Lib.YgoEdo.Data.Card
{
    public class CardLibrary : ICardLibrary
    {
        private static readonly Dictionary<uint, CardData> Cards = new();
        private static bool _initialized;
        public static ICardLibrary Instance { get; private set; }

        public ICardData GetCard(uint code)
        {
            return code == 0 ? CardData.GetEmpty() : Cards[code];
        }

        public static void CreateInstance()
        {
            if (_initialized)
                return;

            Instance = new CardLibrary();
        }

        public static void AddCard(OCG_CardData data, string cardName, string cardText, List<string> strings,
            ulong category)
        {
            if (Cards.ContainsKey(data.code))
                return;
            Cards.Add(data.code, new CardData(data, cardName, cardText, strings, category));
        }

        public bool HasCard(uint code)
        {
            return Cards.ContainsKey(code);
        }

        public static CardData InternalGetCard(uint code)
        {
            return code == 0 ? CardData.GetEmpty() : Cards[code];
        }

        public static IReadOnlyDictionary<uint, CardData> AllCards()
        {
            return Cards;
        }
    }
}