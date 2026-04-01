using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Plastic.Newtonsoft.Json;
using Ygo.Scripts.Cards.Enums;

namespace Ygo.Scripts.Cards
{
    public class MonsterData
    {
        [JsonProperty("attribute")]
        public MonsterAttribute Attribute { get; }
        [JsonProperty("level")]
        public int Level { get; }
        [JsonProperty("monster_type")]
        public MonsterType Type { get; }
        [JsonProperty("monster_kinds")]
        public IList<MonsterKind> Kinds { get; }
        [JsonProperty("atk")]
        public int Atk { get; }
        [JsonProperty("def")]
        public int Def { get; }
        [JsonProperty("pendulum_scale")][CanBeNull]
        public int? PendulumScale { get; }
        [JsonProperty("link_arrows")][CanBeNull]
        public IList<int> LinkArrows { get; }
        [JsonProperty("effect_ids")][CanBeNull]
        public IList<int> EffectIds { get; }
        [JsonProperty("flavor_text")][CanBeNull]
        public string FlavorText { get; }
        
        public MonsterData(
            MonsterAttribute attribute,
            int level,
            MonsterType type,
            List<MonsterKind> kinds,
            int atk,
            int def,
            int pendulumScale,
            List<int> linkArrows,
            List<int> effectIds,
            string flavorText)
        {
            Attribute = attribute;
            Level = level;
            Type = type;
            Kinds = kinds;
            Atk = atk;
            Def = def;
            PendulumScale = pendulumScale;
            LinkArrows = linkArrows;
            EffectIds = effectIds;
            FlavorText = flavorText;
        }
    }
}