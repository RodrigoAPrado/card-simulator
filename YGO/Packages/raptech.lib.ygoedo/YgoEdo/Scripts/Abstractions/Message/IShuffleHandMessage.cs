using System.Collections.Generic;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message
{
    public interface IShuffleHandMessage : IDuelMessage
    {
        byte Player { get; }
        IReadOnlyList<uint> Cards { get; }
    }
}