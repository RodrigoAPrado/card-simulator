using System.Collections.Generic;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.System.Enum;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message
{
    public interface ITossCoinMessage : IDuelMessage
    {
        byte Player { get; }
        IReadOnlyList<CoinResult> Results { get; }
    }
}