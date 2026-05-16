using System.Collections.Generic;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message
{
    public interface IShuffleSetCardMessage : IDuelMessage
    {
        Location Location { get; }
        IReadOnlyList<IFullLocationReference> Cards { get; }
        IReadOnlyList<IFullLocationReference> Xyzs { get; }
    }
}