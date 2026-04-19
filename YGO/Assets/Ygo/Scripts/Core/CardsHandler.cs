using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Ygo.Core.Abstract;
using Ygo.Core.Enums;
using Ygo.Data;
using Random = System.Random;

namespace Ygo.Core
{
    public class CardsHandler
    {
        public IList<ICardInstance> PlayerCards => _playerCards.AsReadOnly();
        public IList<ICardInstance> PlayerHand => _playerHand.AsReadOnly();
        public IList<ICardInstance> MainDeck => _mainDeck.AsReadOnly();
        private List<ICardInstance> _playerCards;
        private List<ICardInstance> _playerHand;
        private List<ICardInstance> _mainDeck;

        public void Setup(ICardRepository repo, Guid ownerId)
        {
            _playerHand = new List<ICardInstance>();
            _mainDeck = CreateDeck(repo, 40, ownerId);
            _playerCards = new List<ICardInstance>();
            foreach (var t in _mainDeck)
            {
                _playerCards.Add(t);
            }
        }
        
        public void ShuffleDeck()
        {
            var rng = new Random();

            for (var i = _mainDeck.Count - 1; i > 0; i--)
            {
                var j = rng.Next(0, i + 1);
                (_mainDeck[i], _mainDeck[j]) = (_mainDeck[j], _mainDeck[i]);
            }

            foreach (var card in _mainDeck)
            {
                Debug.Log(card.Data.Name);
            }
        }

        public void AddCardToDeck(Guid ownerId, CardData cardData, bool top = true)
        {
            var card = new CardInstance(cardData, ownerId);
            _playerCards.Add(card);
            if (top)
            {
                ICardInstance c1 = null;
                ICardInstance c2 = null;
                for (var i = 0; i < _mainDeck.Count; i++)
                {
                    c2 = _mainDeck[i];
                    _mainDeck[i] = c1;
                    c1 = c2;
                }
                _mainDeck[0] = card;
                _mainDeck.Add(c1);
            }
            else
            {
                _mainDeck.Add(card);
            }
        }
        
        public bool TryDrawFromDeck()
        {
            if (_mainDeck.Count <= 0)
            {
                return false;
            }
            var cardToDraw = _mainDeck[0];
            _playerHand.Add(cardToDraw);
            _mainDeck.RemoveAt(0);
            cardToDraw.AddToHand();
            return true;
        }

        public void RemoveCardFromHand(ICardInstance cardInstance)
        {
            _playerHand.Remove(cardInstance);
        }

        private List<ICardInstance> CreateDeck(ICardRepository repo, int deckSize, Guid ownerId)
        {
            var deck = new List<ICardInstance>();

            var cardsIncluded = new Dictionary<string, int>();
            var availableIds = repo.IdsList;
            var rng = new Random();
            _playerHand = new List<ICardInstance>();
            for (var i = 0; i < deckSize; i++)
            {
                CardData data = null;
                do
                {
                    data = repo.GetMainDeckCardById(availableIds[rng.Next(0, availableIds.Count)]);
                    if (data == null || data.Id == "55144522") continue;
                    
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

                var instance = new CardInstance(data, ownerId);
                instance.AddToMainDeck();
                deck.Add(instance);
            }

            return deck.OrderBy(x => x.Data.Id).ToList();
        }
    }
}