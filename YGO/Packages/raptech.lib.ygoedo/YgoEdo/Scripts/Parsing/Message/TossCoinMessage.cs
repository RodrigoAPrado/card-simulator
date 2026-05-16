using System.Collections.Generic;
using System.Linq;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.System.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Constant;
using YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr;
using YgoSoul.RapTech.Lib.YgoEdo.Util;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message
{
    public class TossCoinMessage : BaseMessage, ITossCoinMessage
    {
        private readonly List<OCG_CoinResult> _results;

        public TossCoinMessage(byte player, List<OCG_CoinResult> results)
        {
            Player = player;
            _results = results;
            Results = _results.Select(x => x.ToCoinResult()).ToList().AsReadOnly();
        }

        public byte Player { get; }
        public IReadOnlyList<CoinResult> Results { get; }

        public override string ToString()
        {
            return $"TossCoin, Player={Player}, Results=[{string.Join(", ", _results)}]";
        }
    }
}