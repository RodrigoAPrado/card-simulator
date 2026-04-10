using System;
using System.Collections.Generic;
using Ygo.Core.Abstract;

namespace Ygo.Core
{
    public class PlayerEffects
    {
        public Guid PlayerId { get; }
        public IList<IEffectInstance> Effects { get; }

        public PlayerEffects(Guid playerId, IList<IEffectInstance> effects)
        {
            PlayerId = playerId;
            Effects = effects;
        }
    }
}