using System;
using System.Collections.Generic;
using Ygo.Core.Abstract;
using Ygo.Core.Effects.Conditions.Abstract;
using Ygo.Data.Effect;

namespace Ygo.Core.Effects.Conditions
{
    public class AnyCardInDeckCondition : ICardEffectCondition
    {
        private readonly EffectConditionData _data;
        
        public AnyCardInDeckCondition(EffectConditionData data)
        {
            _data = data;
        }

        public bool CanActivate(Guid playerId, TurnContext context)
        {
            var mainDeck = context.GetPlayerById(playerId).CardsHandler.MainDeck;
            return mainDeck.Count >= _data.Amount;
        }
    }
}