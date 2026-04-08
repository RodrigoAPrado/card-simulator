using System;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Ygo.Data.Enums;

namespace Ygo.Data
{
    public class CardData
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("card_type")]
        public CardType CardType { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("monster_data")][CanBeNull]
        public MonsterData MonsterData { get; set; }
        [JsonProperty("spell_data")][CanBeNull]
        public SpellData SpellData { get; set; }
        [JsonProperty("trap_data")][CanBeNull]
        public SpellData TrapData { get; set; }

        public void Validate()
        {
            switch (CardType)
            {
                case CardType.Monster:
                    if(MonsterData == null)
                        throw new InvalidOperationException("MonsterData cannot be null");
                    MonsterData.Validate();
                    break;
                case CardType.Spell:
                    if(SpellData == null)
                        throw new InvalidOperationException("MonsterData cannot be null");
                    break;
                case CardType.Trap:
                    if(MonsterData == null)
                        throw new InvalidOperationException("MonsterData cannot be null");
                    break;
                default:
                    throw new InvalidOperationException("Invalid card type");
            }
        }
    }
}