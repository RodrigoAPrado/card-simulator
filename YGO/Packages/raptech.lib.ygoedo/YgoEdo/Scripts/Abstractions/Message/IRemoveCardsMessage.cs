using System.Collections.Generic;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message
{
    public interface IRemoveCardsMessage : IDuelMessage
    {
        IReadOnlyList<IFullLocationReference> Locations { get; }
    }
}