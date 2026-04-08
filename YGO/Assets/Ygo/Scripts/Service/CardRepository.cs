using System;
using System.Collections.Generic;
using System.Linq;
using Ygo.Core.Abstract;
using Ygo.Data;
using Ygo.Data.Enums;

namespace Ygo.Service
{
    public sealed class CardRepository : ICardRepository
    {
        private readonly Dictionary<string, CardData> _cards;
        public IList<string> IdsList => _cards.Keys.ToList().AsReadOnly();

        public CardRepository(Dictionary<string, CardData> cards)
        {
            _cards = cards;
        }

        public CardData GetCardById(string id)
        {
            return _cards[id];
        }

        public CardData GetMainDeckCardById(string id)
        {
            var card = _cards[id];
            if (card.CardType != CardType.Monster)
            {
                return card;
            }

            if (card.MonsterData == null)
                throw new InvalidOperationException($"{nameof(card.MonsterData)} cannot be null. Id {id}.");
            
            var kinds = card.MonsterData.Kinds;
            if (kinds.Contains(MonsterKind.Fusion) 
                || kinds.Contains(MonsterKind.Link) 
                || kinds.Contains(MonsterKind.Synchro)
                || kinds.Contains(MonsterKind.Xyz)
                )
            {
                return null;
            }

            return card;
        }
    }
}