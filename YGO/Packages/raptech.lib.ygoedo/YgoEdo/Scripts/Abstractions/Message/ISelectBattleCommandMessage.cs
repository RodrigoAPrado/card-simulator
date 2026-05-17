using System.Collections.Generic;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component.Command.Base;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message
{
    public interface ISelectBattleCommandMessage : ISelectionDuelMessage
    {
        byte Player { get; }
        IReadOnlyList<IBattleCommand> Choices { get; }
    }
}