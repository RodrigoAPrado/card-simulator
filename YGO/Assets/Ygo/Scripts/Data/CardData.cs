using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Plastic.Newtonsoft.Json;
using UnityEditor;
using Ygo.Scripts.Cards.Enums;

namespace Ygo.Scripts.Cards
{
    public class CardData
    {
        [JsonProperty("id")]
        public int Id { get; }
        [JsonProperty("card_type")]
        public CardType CardType { get; }
        [JsonProperty("name")]
        public string Name { get; }
        [JsonProperty("monster_data")][CanBeNull]
        public MonsterData MonsterData { get; }
       

        public CardData(
            int id, 
            CardType cardType, 
            string name,
            MonsterData monsterData)
        {
            Id = id;
            CardType = cardType;
            Name = name;
            MonsterData = monsterData;
        }
    }
}