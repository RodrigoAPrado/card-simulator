using System;
using System.Collections.Generic;
using Ygo.Core.Actions;
using Ygo.Core.Actions.Abstract;
using Ygo.Core.Effects.Abstract;
using Ygo.Core.Effects.Resolution.Abstract;
using Ygo.Data.Effect;
using Ygo.Data.Effect.Enum;

namespace Ygo.Core.Effects.Resolution
{
    public class DrawEffectResolution : ICardEffectResolution
    {
        private readonly EffectResolutionData _data;

        public DrawEffectResolution(EffectResolutionData data)
        {
            _data = data;
        }

        public IList<IGameAction> Resolve(Guid playerId, GameState gameState)
        {
            var actions = new List<IGameAction>();
            for (var i = 0; i < _data.Amount; i++)
            {
                actions.Add(new DelegatedGameAction(() => gameState.DrawForTurn(playerId)));
            }
            return actions;
        }
    }
}