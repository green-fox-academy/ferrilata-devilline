using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ferrilata_devilline.Models.DTOs
{
    public class LevelDTO
    {
        [JsonProperty(PropertyName = "id")]
        public long LevelId { get; set; }

        [JsonProperty(PropertyName = "level")]
        public long LevelNumber { get; set; }

        [JsonProperty(PropertyName = "weight")]
        public string Weight { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "holders")]
        public List<HolderDTO> Holders { get; set; }
    }
}
