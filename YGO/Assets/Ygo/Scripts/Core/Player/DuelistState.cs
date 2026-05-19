using System.Collections.Generic;
using Ygo.Scripts.Data;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card;

namespace Ygo.Scripts.Core.Player
{
    public class DuelistState
    {
        public IReadOnlyList<ICardData> MainDeck => _mainDeck.AsReadOnly();
        public IReadOnlyList<ICardData> Hand => _hand.AsReadOnly();
        public IReadOnlyList<ICardData> Graveyard => _graveyard.AsReadOnly();
        public IReadOnlyList<ICardData> ExtraDeck => _extraDeck.AsReadOnly();
        public IReadOnlyList<ICardData> Banished => _banished.AsReadOnly();

        private List<ICardData> _mainDeck;
        private List<ICardData> _hand;
        private List<ICardData> _graveyard;
        private List<ICardData> _extraDeck;
        private List<ICardData> _banished;

        public DuelistState(DuelistData data)
        {
            _mainDeck = new List<ICardData>();
            _mainDeck.AddRange(data.MainDeck);
            _hand = new List<ICardData>();
            _graveyard = new List<ICardData>();
            _extraDeck = new List<ICardData>();
            _extraDeck.AddRange(data.ExtraDeck);
            _banished = new List<ICardData>();
        }
    }
}