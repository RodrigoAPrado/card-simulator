using System.Collections.Generic;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message
{
    public interface IFieldDisabledMessage : IDuelMessage
    {
        IReadOnlyList<FieldZones> Zones { get; }
    }
}