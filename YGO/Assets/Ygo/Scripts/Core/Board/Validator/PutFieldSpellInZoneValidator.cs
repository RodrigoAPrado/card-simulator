using System;
using Ygo.Core.Abstract;
using Ygo.Core.Board.Abstract;
using Ygo.Data.Enums;

namespace Ygo.Core.Board.Validator
{
    public class PutFieldSpellInZoneValidator : IPutCardInZoneValidator
    {
        public bool Validate(ICardInstance cardInstance)
        {
            return cardInstance.Data.CardType switch
            {
                CardType.Trap => false,
                CardType.Spell when !cardInstance.IsValidSpell 
                    => throw new InvalidOperationException("Spell is invalid"),
                CardType.Spell => cardInstance.IsField,
                CardType.Monster => false,
                _ => throw new InvalidOperationException("Card Type is invalid")
            };
        }
    }
}