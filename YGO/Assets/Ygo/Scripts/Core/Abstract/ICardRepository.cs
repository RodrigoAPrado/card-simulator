using System.Collections.Generic;
using Ygo.Data;

namespace Ygo.Core.Abstract
{
    public interface ICardRepository
    {
        IList<int> IdsList { get; }
        CardData GetCardById(int id);
        CardData GetMainDeckCardById(int id);
    }
}