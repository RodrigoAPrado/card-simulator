using System.Collections.Generic;
using Newtonsoft.Json;
using Ygo.Data.Effect;

namespace Ygo.Data
{
    public class CardEffectData
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("effects")]
        public IList<EffectData> Effects { get; set; }
    }
}