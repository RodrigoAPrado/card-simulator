using System.Collections.Generic;
using Ygo.Data;
using Ygo.Data.Effect;

namespace Ygo.Core.Abstract
{
    public interface ICardEffectRepository
    {
        IList<string> IdsList { get; }
        CardEffectData GetEffectById(string id);
    }
}