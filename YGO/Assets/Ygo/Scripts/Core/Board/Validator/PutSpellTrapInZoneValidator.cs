using System;
using Ygo.Core.Abstract;
using Ygo.Core.Board.Abstract;
using Ygo.Data.Enums;

namespace Ygo.Core.Board.Validator
{
    public class PutSpellTrapInZoneValidator : IPutCardInZoneValidator
    {
        public bool Validate(ICardInstance cardInstance)
        {
            return cardInstance.Data.CardType switch
            {
                CardType.Trap => true,
                CardType.Spell when !cardInstance.IsValidSpell 
                    => throw new InvalidOperationException("Spell is invalid"),
                CardType.Spell => !cardInstance.IsField,
                CardType.Monster => cardInstance.TreatedAsSpell || cardInstance.TreatedAsTrap,
                _ => throw new InvalidOperationException("Card Type is invalid")
            };
        }
    }
}