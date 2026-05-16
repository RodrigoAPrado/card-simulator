using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component.Command.Base;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Component.Command.Base
{
    public interface IIdleCmdChoice : IIdleCommand
    {
        PlayerIdleAction Action { get; }
    }
}