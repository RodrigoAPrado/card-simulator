using Newtonsoft.Json;

namespace Ygo.Editor.Model
{
    public class CardImageCardModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }
        [JsonProperty("image_url_small")]
        public string ImageUrlSmall { get; set; }
        [JsonProperty("image_url_cropped")]
        public string ImageUrlCropped { get; set; }
    }
}