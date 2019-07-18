using ferrilata_devilline.Models.DTOs.Input;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ferrilata_devilline.Models.DTOs
{
    public class BadgeDTO
    {
        [JsonProperty(PropertyName = "id", Required = Required.Always)]
        public long BadgeId { get; set; }

        [JsonProperty(PropertyName = "version", Required = Required.Always)]
        public double Version { get; set; }

        [JsonProperty(PropertyName = "name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "tag", Required = Required.Always)]
        public string Tag { get; set; }

        [JsonProperty(PropertyName = "levels", Required = Required.Always)]
        public List<LevelWithoutHoldersDTO> Levels { get; set; }
    }
}
