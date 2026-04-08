using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Ygo.Data.Enums;

namespace Ygo.Data
{
    public class MonsterData
    {
        [JsonProperty("attribute")]
        public MonsterAttribute Attribute { get; set; }
        [JsonProperty("type")]
        public MonsterType Type { get; set; }
        [JsonProperty("kinds")]
        public IList<MonsterKind> Kinds { get; set; }
        [JsonProperty("atk")]
        public int Atk { get; set; }
        [JsonProperty("def")]
        public int Def { get; set; }
        [JsonProperty("level")][CanBeNull]
        public int? Level { get; set; }
        [JsonProperty("rank")][CanBeNull]
        public int? Rank { get; set; }
        [JsonProperty("pendulum_scale")][CanBeNull]
        public int? PendulumScale { get; set; }
        [JsonProperty("link_arrows")][CanBeNull]
        public IList<int> LinkArrows { get; set; }
        [JsonProperty("effect_ids")][CanBeNull]
        public IList<int> EffectIds { get; set; }
        [JsonProperty("flavor_text")][CanBeNull]
        public string FlavorText { get; set; }

        public void Validate()
        {
            if(Attribute == MonsterAttribute.Unknown)
                throw new InvalidOperationException("Attribute is unknown");
            if(Type == MonsterType.Unknown)
                throw new InvalidOperationException("Type is unknown");
        }
    }
}