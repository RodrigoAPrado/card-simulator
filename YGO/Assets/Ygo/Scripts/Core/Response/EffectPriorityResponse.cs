using System;
using System.Collections.Generic;
using Ygo.Core.Abstract;

namespace Ygo.Core.Response
{
    public class EffectPriorityResponse
    {
        public IList<PlayerEffects> Effects { get; }
        
        public EffectPriorityResponse(List<PlayerEffects> effects)
        {
            Effects = effects;
        }
    }
}