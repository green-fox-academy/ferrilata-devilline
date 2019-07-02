using System.Collections.Generic;
using Newtonsoft.Json;


namespace ferrilata_devilline.Models.DTOs
{
    public class AdminDTO
    {
        [JsonProperty (PropertyName = "version")]
        public string Version { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "tag")]
        public string Tag { get; set; }

        [JsonProperty(PropertyName = "levels")]
        public List<object> Levels { get; set; }
    }
}
