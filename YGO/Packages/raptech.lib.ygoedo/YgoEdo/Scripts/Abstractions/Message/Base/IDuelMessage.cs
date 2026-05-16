using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.System.Enum;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base
{
    public interface IDuelMessage
    {
        int InputCount { get; }
        InputType Input { get; }
    }
}