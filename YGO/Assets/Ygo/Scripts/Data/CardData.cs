using System;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Ygo.Data.Enums;

namespace Ygo.Data
{
    public class CardData
    {
        [JsonProperty("id")]
        public string Id { get; }
        [JsonProperty("card_type")]
        public CardType CardType { get; }
        [JsonProperty("name")]
        public string Name { get; }
        [JsonProperty("monster_data")][CanBeNull]
        public MonsterData MonsterData { get; }
        [JsonProperty("spell_data")][CanBeNull]
        public SpellData SpellData { get; }
        [JsonProperty("trap_data")][CanBeNull]
        public SpellData TrapData { get; }

        private void Validate()
        {
            switch (CardType)
            {
                case CardType.Monster:
                    if(MonsterData == null)
                        throw new InvalidOperationException("MonsterData cannot be null");
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