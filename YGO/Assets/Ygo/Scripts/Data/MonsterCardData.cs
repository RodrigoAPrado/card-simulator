using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json;
using Ygo.Scripts.Cards.Enums;

namespace Ygo.Scripts.Cards
{
    public class MonsterCardData
    {
        [JsonProperty("id")]
        public int Id { get; }
        [JsonProperty("card_type")]
        public CardType CardType { get; }
        [JsonProperty("name")]
        public string Name { get; }
        [JsonProperty("attribute")]
        public MonsterAttribute Attribute { get; }
        [JsonProperty("level")]
        public int Level { get; }
        [JsonProperty("monster_type")]
        public MonsterType MonsterType { get; }
        [JsonProperty("monster_kinds")]
        public IList<MonsterKind> MonsterKinds { get; }
        [JsonProperty("atk")]
        public int Atk { get; }
        [JsonProperty("def")]
        public int Def { get; }
        [JsonProperty("pendulum_scale")]
        public int PendulumScale { get; }
        [JsonProperty("link_arrows")]
        public IList<int> LinkArrows { get; }
        [JsonProperty("effect_ids")]
        public IList<int> EffectIds { get; }
        [JsonProperty("flavor_text")]
        public string FlavorText { get; }

        public MonsterCardData(
            int id, 
            CardType cardType, 
            string name,
            MonsterAttribute attribute,
            int level,
            MonsterType monsterType,
            List<MonsterKind> monsterKinds,
            int atk,
            int def,
            int pendulumScale,
            List<int> linkArrows,
            List<int> effectIds,
            string flavorText)
        {
            Id = id;
            CardType = cardType;
            Name = name;
            Attribute = attribute;
            Level = level;
            MonsterType = monsterType;
            MonsterKinds = monsterKinds;
            Atk = atk;
            Def = def;
            PendulumScale = pendulumScale;
            LinkArrows = linkArrows;
            EffectIds = effectIds;
            FlavorText = flavorText;
        }
    }
}