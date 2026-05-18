using System.Collections.Generic;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message
{
    public interface IDrawMessage : IDuelMessage
    {
        byte Player { get; }
        IReadOnlyList<IDrawnCard> Cards { get; }
    }
}