using System.Collections.Generic;
using Ygo.Core.Abstract;
using Ygo.Data;

namespace Ygo.Service
{
    public class CardEffectRepository : ICardEffectRepository
    {
        private readonly Dictionary<string, CardEffectData> _effects;
        public IList<string> IdsList { get; }
        
        public CardEffectRepository(Dictionary<string, CardEffectData> effects)
        {
            _effects = effects;
        }
        
        public CardEffectData GetEffectById(string id)
        {
            return _effects.GetValueOrDefault(id);
        }
    }
}