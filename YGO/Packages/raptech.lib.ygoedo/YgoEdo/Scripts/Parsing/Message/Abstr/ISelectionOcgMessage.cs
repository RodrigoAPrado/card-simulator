using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr
{
    public interface ISelectionOcgMessage : IOcgMessage, ISelectionDuelMessage
    {
        byte[] Cancel();
    }
}