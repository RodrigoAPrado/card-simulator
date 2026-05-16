using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel;
using YgoSoul.RapTech.Lib.YgoEdo.Domain.Bridge;
using YgoSoul.RapTech.Lib.YgoEdo.Data.Card;
using YgoSoul.RapTech.Lib.YgoEdo.Domain.Duel;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing;

namespace YgoSoul.RapTech.Lib.YgoEdo.Api
{
    public static class YgoEdo
    {
        private static IDuelManager _manager;
        private static string _dataPath;
        private static bool _initialized;

        public static IDuelManager Init(string dataPath)
        {
            if (_initialized) return _manager;

            _dataPath = dataPath;
            OcgBridge.Init(_dataPath);
            CardDatabase.Init(_dataPath + "db/cards.cdb");
            CardDatabase.LoadCards();
            CardLibrary.CreateInstance();
            _manager = new DuelManager(CardLibrary.Instance, MessageParserRegistry.RegisterParsers());
            _initialized = true;
            return _manager;
        }
    }
}