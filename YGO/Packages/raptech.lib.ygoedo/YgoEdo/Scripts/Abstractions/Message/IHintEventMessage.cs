using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.System.Enum;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message
{
    public interface IHintEventMessage : IDuelMessage
    {
        byte Player { get; }
        SystemStrings Text { get; }
    }
}