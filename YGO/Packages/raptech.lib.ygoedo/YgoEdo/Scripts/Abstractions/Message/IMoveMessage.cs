using System.Collections.Generic;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.System.Enum;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message
{
    public interface IMoveMessage : IDuelMessage
    {
        uint CardCode { get; }
        IFullLocationReference OldLocation { get; }
        IFullLocationReference NewLocation { get; }
        IReadOnlyList<SystemReason> Reasons { get; }
    }
}