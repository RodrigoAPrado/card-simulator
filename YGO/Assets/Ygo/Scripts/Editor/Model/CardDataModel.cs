using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ygo.Editor.Model
{
    public class CardDataModel
    {
        [JsonProperty("data")]
        public List<CardModel> Data { get; set; }
    }
}