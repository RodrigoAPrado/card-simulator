using System.Collections.Generic;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message
{
    public interface ISelectTributeMessage : ISelectionDuelMessage
    {
        byte Player { get; }
        uint Min { get; }
        uint Max { get; }
        IReadOnlyList<ICardReference> Cards { get; }
    }
}