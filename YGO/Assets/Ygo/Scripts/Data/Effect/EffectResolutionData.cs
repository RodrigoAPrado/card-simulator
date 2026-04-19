using Newtonsoft.Json;
using Ygo.Data.Effect.Enum;

namespace Ygo.Data.Effect
{
    public class EffectResolutionData
    {
        [JsonProperty("type")]
        public ResolutionType Type { get; set; }
        [JsonProperty("amount")]
        public int Amount { get; set; }
    }
}