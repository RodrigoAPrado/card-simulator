using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Ygo.Core.Abstract;
using Ygo.Data;
using Random = System.Random;

namespace Ygo.Core
{
    public class CardsHandler
    {
        public IList<ICardInstance> CardsDrawn => _cardsDrawn.AsReadOnly();
        public IList<ICardInstance> Deck => _deck.AsReadOnly();
        private List<ICardInstance> _cardsDrawn;
        private List<ICardInstance> _deck;

        public void Setup(ICardRepository repo)
        {
            _cardsDrawn = new List<ICardInstance>();
            _deck = CreateDeck(repo, 40);
        }
        
         public void ShuffleDeck()
        {
            var rng = new Random();

            for (var i = _deck.Count - 1; i > 0; i--)
            {
                var j = rng.Next(0, i + 1);
                (_deck[i], _deck[j]) = (_deck[j], _deck[i]);
            }

            foreach (var card in _deck)
            {
                Debug.Log(card.Data.Name);
            }
        }
        
        public bool DrawCards(int amount)
        {
            for (var i = 0; i < amount; i++)
            {
                if (_deck.Count == 0)
                {
                    Debug.Log("OVERDECK!");
                    return false;
                }
                var cardToDraw = _deck[0];
                _cardsDrawn.Add(cardToDraw);
                _deck.RemoveAt(0);
            }
            return true;
        }

        private List<ICardInstance> CreateDeck(ICardRepository repo, int deckSize)
        {
            var deck = new List<ICardInstance>();

            var cardsIncluded = new Dictionary<int, int>();
            var availableIds = repo.IdsList;
            var rng = new Random();
            _cardsDrawn = new List<ICardInstance>();
            for (var i = 0; i < deckSize; i++)
            {
                CardData data = null;
                do
                {
                    data = repo.GetMainDeckCardById(availableIds[rng.Next(0, availableIds.Count)]);
                    if (data == null) continue;
                    
                    if (!cardsIncluded.ContainsKey(data.Id))
                    {
                        cardsIncluded.Add(data.Id, 1);
                    }
                    else
                    {
                        if (cardsIncluded[data.Id] >= 3)
                        {
                            data = null;
                        }
                        else
                        {
                            var amount = cardsIncluded[data.Id];
                            cardsIncluded[data.Id] = amount+1;
                        }
                    }
                } while (data == null);
                
                deck.Add(new CardInstance(data));
            }

            return deck.OrderBy(x => x.Data.Id).ToList();
        }
    }
}