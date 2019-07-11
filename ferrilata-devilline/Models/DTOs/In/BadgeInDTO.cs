using ferrilata_devilline.Models.DTOs.Out;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ferrilata_devilline.Models.DTOs
{
    public class BadgeInputDTO
    {
            [JsonProperty(PropertyName = "version")]
            public double Version { get; set; }

            [JsonProperty(PropertyName = "name")]
            public string Name { get; set; }

            [JsonProperty(PropertyName = "tag")]
            public string Tag { get; set; }

            [JsonProperty(PropertyName = "levels")]
            public List<LevelInDTO> Levels { get; set; }
    }
}