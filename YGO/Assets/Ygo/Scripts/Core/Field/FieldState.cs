using System;
using System.Collections.Generic;
using Ygo.Scripts.Core.Card;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Enum;

namespace Ygo.Scripts.Core.Field
{
    public class FieldState
    {
        public IReadOnlyDictionary<FieldZones, CardState> CardStates => _cardStates;
        private Dictionary<FieldZones, CardState> _cardStates;

        public FieldState()
        {
            _cardStates = new Dictionary<FieldZones, CardState>();
        }

        public CardState TryGetCardStateFromCardCode(uint cardCode)
        {
            foreach (var cardState in _cardStates)
            {
                if (cardState.Value.Data.Code == cardCode)
                    return cardState.Value;
            }

            throw new ArgumentOutOfRangeException($"No card with cardCode {cardCode} on field!");
        }
    }
}