using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.System.Enum;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message
{
    public interface IHandResMessage : IDuelMessage
    {
        RockPaperScissors Player0 { get; }
        RockPaperScissors Player1 { get; }
    }
}