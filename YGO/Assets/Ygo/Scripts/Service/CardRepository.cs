using System.Collections.Generic;
using Ygo.Scripts.Cards;
using Ygo.Scripts.Core;

namespace Ygo.Service
{
    public sealed class CardRepository : ICardRepository
    {
        private Dictionary<int, CardData> Cards { get; }

        public CardRepository(Dictionary<int, CardData> cards)
        {
            Cards = cards;
        }
        
        public CardData GetCardData(int id)
        {
            return Cards[id];
        }
    }
}