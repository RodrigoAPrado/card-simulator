using System.Collections.Generic;
using Ygo.Scripts.Cards;

namespace Ygo.Service
{
    public sealed class CardDatabase
    {
        private Dictionary<int, MonsterCardData> Cards { get; }

        public CardDatabase(Dictionary<int, MonsterCardData> cards)
        {
            Cards = cards;
        }
        
        public MonsterCardData GetMonsterCardData(int id)
        {
            return Cards[id];
        }
    }
}