using System.Collections.Generic;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message
{
    public interface IAnnounceNumberMessage : ISelectionDuelMessage
    {
        byte Player { get; }
        IReadOnlyList<uint> AvailableNumbers { get; }
    }
}