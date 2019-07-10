using Newtonsoft.Json;
using System.Collections.Generic;

namespace ferrilata_devilline.Models.DAOs
{
    public class Level
    {
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        [JsonProperty(PropertyName = "level")]
        public int LevelNumber { get; set; }

        [JsonProperty(PropertyName = "weight")]
        public int Weight { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "holders")]
        public List<Holder> Holders { get; set; }
    }
}
