using System;
using System.Collections.Generic;
using System.Linq;
using Ygo.Core.Effects.Conditions;
using Ygo.Core.Effects.Conditions.Abstract;
using Ygo.Core.Effects.Resolution;
using Ygo.Core.Effects.Resolution.Abstract;
using Ygo.Data.Effect;
using Ygo.Data.Effect.Enum;

namespace Ygo.Core.Effects.Abstract
{
    public static class CardEffectFactory
    {
        
        public static ICardEffect CreateEffectFromData(
            string cardId,
            EffectData data
            )
        {
            var conditionsList = CreateEffectConditionsList(data.Conditions);
            var resolution = CreateEffectResolution(data.Resolution);
            return new BasicCardEffect(cardId, data, conditionsList, resolution);
        }

        private static IList<ICardEffectCondition> CreateEffectConditionsList(
            IList<EffectConditionData> data
            )
        {
            return data.Select(CreateEffectCondition).ToList();
        }

        private static ICardEffectCondition CreateEffectCondition( 
            EffectConditionData data
            )
        {
            switch (data.Type)
            {
                case ConditionType.AnyCardInDeck:
                    return new AnyCardInDeckCondition(data);
                case ConditionType.Unknown:
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static ICardEffectResolution CreateEffectResolution(
            EffectResolutionData data
            )
        {
            ICardEffectResolution resolution;
            
            switch (data.Type)
            {
                case ResolutionType.DrawCard:
                    resolution = new DrawEffectResolution(data);
                    break;
                case ResolutionType.Unknown:
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return resolution;
        }
    }
}