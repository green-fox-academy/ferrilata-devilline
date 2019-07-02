using Newtonsoft.Json;
using System.Collections.Generic;

namespace ferrilata_devilline.Models.DAOs
{
    public class Badge
    {
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        [JsonProperty(PropertyName = "version")]
        public double Version { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "tag")]
        public string Tag { get; set; }

        [JsonProperty(PropertyName = "levels")]
        public List<Level> Levels { get; set; }
    }
}
