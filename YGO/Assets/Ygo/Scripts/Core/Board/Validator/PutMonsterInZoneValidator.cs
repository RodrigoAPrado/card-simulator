using System;
using Ygo.Core.Abstract;
using Ygo.Core.Board.Abstract;
using Ygo.Data.Enums;

namespace Ygo.Core.Board.Validator
{
    public class PutMonsterInZoneValidator : IPutCardInZoneValidator
    {
        public bool Validate(ICardInstance cardInstance)
        {
            return cardInstance.Data.CardType switch
            {
                CardType.Trap => cardInstance.TreatedAsMonster,
                CardType.Spell => cardInstance.TreatedAsMonster,
                CardType.Monster => true,
                _ => throw new InvalidOperationException("Card Type is invalid")
            };
        }
    }
}