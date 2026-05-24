using System;
using System.Collections.Generic;
using System.Linq;
using Ygo.Scripts.Core.Card;
using Ygo.Scripts.Data;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component;

namespace Ygo.Scripts.Core.Duelist
{
    public class DuelistState
    {
        public IReadOnlyList<CardState> MainDeck => _mainDeck.AsReadOnly();
        public IReadOnlyList<CardState> Hand => _hand.AsReadOnly();
        public IReadOnlyList<CardState> Graveyard => _graveyard.AsReadOnly();
        public IReadOnlyList<CardState> ExtraDeck => _extraDeck.AsReadOnly();
        public IReadOnlyList<CardState> Banished => _banished.AsReadOnly();
        public byte Player { get; }

        private List<CardState> _mainDeck;
        private List<CardState> _hand;
        private List<CardState> _graveyard;
        private List<CardState> _extraDeck;
        private List<CardState> _banished;

        public DuelistState(DuelistData data, byte player)
        {
            _mainDeck = new List<CardState>();
            _mainDeck.AddRange(data.MainDeck.Select(x => new CardState(x, Location.Deck, player)));
            _hand = new List<CardState>();
            _graveyard = new List<CardState>();
            _extraDeck = new List<CardState>();
            _extraDeck.AddRange(data.ExtraDeck.Select(x => new CardState(x, Location.Extra, player)));
            _banished = new List<CardState>();
            Player = player;
        }

        public CardState GetCardByCode(uint cardCode, Location location)
        {
            switch (location)
            {
                case Location.Deck:
                    return _mainDeck.FirstOrDefault(x => x.Data.Code == cardCode);
                case Location.Hand:
                    return _hand.FirstOrDefault(x => x.Data.Code == cardCode);
                case Location.Grave:
                    return _graveyard.FirstOrDefault(x => x.Data.Code == cardCode);
                case Location.Banishment:
                    return _banished.FirstOrDefault(x => x.Data.Code == cardCode);
                case Location.Extra:
                    return _extraDeck.FirstOrDefault(x => x.Data.Code == cardCode);
                default:
                    throw new ArgumentOutOfRangeException(nameof(location), location, null);
            }
        }
        
        public void PutCard(CardState card, Location location)
        {
            switch (location)
            {
                case Location.Deck:
                    _mainDeck.Add(card);
                    break;
                case Location.Hand:
                    _hand.Add(card);
                    break;
                case Location.Grave:
                    _graveyard.Add(card);
                    break;
                case Location.Banishment:
                    _banished.Add(card);
                    break;
                case Location.Extra:
                    _extraDeck.Add(card);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(location), location, null);
            }
        }

        public CardState TakeCard(uint cardCode, Location location, int sequence)
        {
            CardState card;
            List<CardState> list;

            switch (location)
            {
                case Location.Deck:
                    list = _mainDeck;
                    break;
                case Location.Hand:
                    list = _hand;
                    break;
                case Location.Grave:
                    list = _graveyard;
                    break;
                case Location.Banishment:
                    list = _banished;
                    break;
                case Location.Extra:
                    list = _extraDeck;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(location), location, null);
            }
            
            card = location == Location.Deck ? list.FirstOrDefault(x => x.Data.Code == cardCode) : list[sequence];
            
            if (card == null)
                throw new InvalidOperationException("No card found");
            if (card.Data.Code != cardCode)
                throw new InvalidOperationException($"Given card does not match! Sequence:{sequence}, Code:{cardCode}, ActualCard:{card.Data.Code}");
            
            list.Remove(card);
            
            return card;
        }
    }
}