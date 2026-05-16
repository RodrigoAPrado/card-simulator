using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message
{
    public interface ICancelTargetMessage
    {
        IFullLocationReference Card { get; }
        IFullLocationReference Target { get; }
    }
}