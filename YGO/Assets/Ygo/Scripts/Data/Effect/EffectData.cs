using System.Collections.Generic;
using Newtonsoft.Json;
using Ygo.Data.Effect.Enum;

namespace Ygo.Data.Effect
{
    public class EffectData
    {
        [JsonProperty("activation")]
        public EffectActivationType Activation { get; set; }
        [JsonProperty("conditions")]
        public IList<EffectConditionData> Conditions { get; set; }
        [JsonProperty("cost")]
        public EffectCostData Cost { get; set; }
        [JsonProperty("resolution")]
        public EffectResolutionData Resolution { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}