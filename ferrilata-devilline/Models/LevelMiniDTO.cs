using Newtonsoft.Json;

namespace ferrilata_devilline.Models.DTOs
{
    public class LevelMiniDTO
    {
        [JsonProperty(PropertyName = "levelId", Required = Required.Always)]
        public long LevelId { get; set; }

        [JsonProperty(PropertyName = "level", Required = Required.Always)]
        public int LevelNumber { get; set; }

    }
}
