using System.Collections.Generic;
using Ygo.Data;

namespace Ygo.Core.Abstract
{
    public interface ICardRepository
    {
        IList<string> IdsList { get; }
        CardData GetCardById(string id);
        CardData GetMainDeckCardById(string id);
    }
}