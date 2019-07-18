using ferrilata_devilline.Models.DTOs.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ferrilata_devilline.IntegrationTests.Fixtures.Models
{
    public class BadgeDTOWithNullValues
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
        public List<LevelWithoutHoldersDTO> Levels { get; set; }
    }
}
