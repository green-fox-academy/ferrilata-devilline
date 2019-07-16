using Newtonsoft.Json;

namespace ferrilata_devilline.Models.DTOs
{
    public class LevelInDTO
    {
        [JsonProperty(PropertyName = "level", Required = Required.Always)]
        public int LevelNumber { get; set; }

        [JsonProperty(PropertyName = "weight", Required = Required.Always)]
        public string Weight { get; set; }

        [JsonProperty(PropertyName = "description", Required = Required.Always)]
        public string Description { get; set; }
    }
}
