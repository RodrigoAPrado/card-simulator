using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ygo.Editor.Model
{
    public class CardModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("typeline")]
        public List<string> TypeLine { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("humanReadableCardType")]
        public string HumanReadableCardType { get; set; }
        [JsonProperty("frameType")]
        public string FrameType { get; set; }
        [JsonProperty("desc")]
        public string Desc { get; set; }
        [JsonProperty("race")]
        public string Race { get; set; }
        [JsonProperty("atk")]
        public int Atk { get; set; }
        [JsonProperty("def")]
        public int Def { get; set; }
        [JsonProperty("level")]
        public int Level { get; set; }
        [JsonProperty("attribute")]
        public string Attribute { get; set; }
        [JsonProperty("ygoprodeck_url")]
        public string YgoProDeckUrl { get; set; }
        [JsonProperty("card_sets")]
        public List<CardSetCardModel> CardSets { get; set; }
        [JsonProperty("card_images")]
        public List<CardImageCardModel> CardImages { get; set; }
        
        
    }
}