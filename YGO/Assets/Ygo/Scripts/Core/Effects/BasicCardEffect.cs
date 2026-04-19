using System;
using System.Collections.Generic;
using System.Linq;
using Ygo.Core.Effects.Abstract;
using Ygo.Core.Effects.Conditions.Abstract;
using Ygo.Core.Effects.Resolution.Abstract;
using Ygo.Data.Effect;

namespace Ygo.Core.Effects
{
    public class BasicCardEffect : ICardEffect
    {
        public Guid Id { get; }
        public string CardId { get; }
        public string Description => _data.Text;
        private readonly EffectData _data;
        private readonly IList<ICardEffectCondition> _conditions;
        private readonly ICardEffectResolution _resolution;

        public BasicCardEffect(string cardId, EffectData data, IList<ICardEffectCondition> conditions, ICardEffectResolution resolution)
        {
            Id = Guid.NewGuid();
            CardId = cardId;
            _data = data;
            _conditions = conditions;
            _resolution = resolution;
        }
        
        
        public void ApplyCost()
        {
            //
        }

        public void Resolve(Guid playerId, GameState gameState)
        {
            var actions = _resolution.Resolve(playerId, gameState);
            //
        }

        public bool CanActivate(Guid playerId, TurnContext context)
        {
            return _conditions.All(c => c.CanActivate(playerId, context));
        }
    }
}