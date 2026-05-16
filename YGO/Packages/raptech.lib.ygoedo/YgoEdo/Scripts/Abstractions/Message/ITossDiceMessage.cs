using System.Collections.Generic;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message
{
    public interface ITossDiceMessage : IDuelMessage
    {
        byte Player { get; }
        IReadOnlyList<byte> Results { get; }
    }
}