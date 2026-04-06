using Newtonsoft.Json;
using Ygo.Data.Enums;

namespace Ygo.Data
{
    public class TrapData
    {
        [JsonProperty("type")]
        public TrapType Type { get; }

        public TrapData(TrapType type)
        {
            Type = type;
        }
    }
}