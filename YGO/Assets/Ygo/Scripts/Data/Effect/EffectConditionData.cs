using Newtonsoft.Json;
using Ygo.Data.Effect.Enum;

namespace Ygo.Data.Effect
{
    public class EffectConditionData
    {
        [JsonProperty("type")]
        public ConditionType Type { get; set; }
        [JsonProperty("amount")]
        public int Amount { get; set; }
    }
}