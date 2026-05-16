using System.Collections.Generic;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Message.Abstr
{
    public interface IOcgMessage : IDuelMessage
    {
        byte[] GetResponse(List<int> input);
    }
}