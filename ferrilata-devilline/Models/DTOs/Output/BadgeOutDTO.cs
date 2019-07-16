using Newtonsoft.Json;
using System.Collections.Generic;

namespace ferrilata_devilline.Models.DTOs
{
    public class BadgeOutDTO
    {
        [JsonProperty(PropertyName = "id")]
        public long BadgeId { get; set; }

        [JsonProperty(PropertyName = "version")]
        public double Version { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "tag")]
        public string Tag { get; set; }

        [JsonProperty(PropertyName = "levels")]
        public List<LevelOutDTO> Levels { get; set; }
    }
}
