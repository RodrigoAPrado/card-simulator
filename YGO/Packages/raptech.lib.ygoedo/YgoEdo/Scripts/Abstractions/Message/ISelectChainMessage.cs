using System.Collections.Generic;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.System.Enum;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message
{
    public interface ISelectChainMessage : ISelectionDuelMessage
    {
        byte Player { get; }
        bool Forced { get; }
        IReadOnlyList<IChainOption> Effects { get; }
        IReadOnlyList<HintTiming> Timing { get; }
        IReadOnlyList<HintTiming> TimingOther { get; }
    }
}