using JetBrains.Annotations;
using Newtonsoft.Json;
using Ygo.Data.Enums;

namespace Ygo.Data
{
    public class SpellData
    {
        [JsonProperty("type")]
        public SpellType Type { get; }

        public SpellData(SpellType type)
        {
            Type = type;
        }
    } 
}