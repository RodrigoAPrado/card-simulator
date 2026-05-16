using System.Collections.Generic;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message
{
    public interface ISelectOptionMessage : ISelectionDuelMessage
    {
        byte Player { get; }
        IReadOnlyList<string> Options { get; }
    }
}