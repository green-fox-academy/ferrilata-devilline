using Newtonsoft.Json;

namespace ferrilata_devilline.Models.DTOs.Out
{
    public class LevelInDTO
    {
        [JsonProperty(PropertyName = "level")]
        public int LevelNumber { get; set; }

        [JsonProperty(PropertyName = "weight")]
        public string Weight { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
    }
}