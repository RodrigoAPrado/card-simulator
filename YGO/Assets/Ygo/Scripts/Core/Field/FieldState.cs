using System;
using System.Collections.Generic;
using Ygo.Scripts.Core.Card;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Flag;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Component;

namespace Ygo.Scripts.Core.Field
{
    public class FieldState
    {
        public IReadOnlyDictionary<FieldZones, CardState> CardStates => _cardStates;
        private readonly Dictionary<FieldZones, CardState> _cardStates;

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

        public CardState TakeCard(uint cardCode, IFullLocationReference location, byte playerId)
        {
            var fieldZone = GetFieldZone(location, playerId);
            if (fieldZone == FieldZones.Outside)
                throw new InvalidOperationException("Outside field zone!");
            var card = _cardStates.GetValueOrDefault(fieldZone);
            
            if (card == null)
                throw new InvalidOperationException("No card found");
            if (card.Data.Code != cardCode)
                throw new InvalidOperationException($"Given card does not match! " +
                                                    $"Sequence:{location.Sequence}, " +
                                                    $"Code:{cardCode}, " +
                                                    $"ActualCard:{card.Data.Code}");
            _cardStates.Remove(fieldZone);
            return card;
        }

        public void PutCard(CardState card, IFullLocationReference location, byte playerId)
        {
            var fieldZone = GetFieldZone(location, playerId);
            if (fieldZone == FieldZones.Outside)
                throw new InvalidOperationException("Outside field zone!");

            if (!_cardStates.TryAdd(fieldZone, card))
                throw new InvalidOperationException("Card Already In Place!");
        }

        public FieldZones GetFieldZone(IFullLocationReference location, byte playerId)
        {
            var zoneValue = 0;
            switch (location.Location)
            {
                case Location.MonsterZone:
                    zoneValue += 0;
                    break;
                case Location.SpellTrapZone:
                    zoneValue += 10;
                    break;
                default:
                    return FieldZones.Outside;
            }
            zoneValue += (int)location.Sequence;
            zoneValue += location.Controller == playerId ? 0 : 100;
            
            return (FieldZones)zoneValue;
        }
    }
}