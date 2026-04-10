using System.Collections.Generic;
using Ygo.Core.Response;

namespace Ygo.Core
{
    public class EffectPriorityContext
    {
        private TurnContext _turnContext;
        public EffectPriorityContext(TurnContext turnContext)
        {
            _turnContext = turnContext;
        }

        public IList<PlayerEffects> GetEffects()
        {
            return new List<PlayerEffects>();
        }
    }
}