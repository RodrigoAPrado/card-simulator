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
    }
}