using System.Collections.Generic;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message
{
    public interface IAnnounceCardMessage : ISelectionDuelMessage
    {
        byte Player { get; }
        IReadOnlyList<(string, uint)> AvailableCards { get; }
        List<(int, string)> Query(string value);
    }
}