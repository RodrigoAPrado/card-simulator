using System.Collections.Generic;
using System.Linq;
using Ygo.Core.Abstract;
using Ygo.Data;

namespace Ygo.Service
{
    public sealed class CardRepository : ICardRepository
    {
        private readonly Dictionary<int, CardData> _cards;
        public IList<int> IdsList => _cards.Keys.ToList().AsReadOnly();

        public CardRepository(Dictionary<int, CardData> cards)
        {
            _cards = cards;
        }

        public CardData GetCardById(int id)
        {
            return _cards[id];
        }
    }
}