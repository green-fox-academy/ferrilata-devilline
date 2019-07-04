using System.Collections.Generic;
using Newtonsoft.Json;


namespace ferrilata_devilline.Models.DTOs
{
    public class AdminDTO
    {
        [JsonProperty (PropertyName = "version", Required = Required.Always)]
        public string Version { get; set; }

        [JsonProperty(PropertyName = "name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "tag", Required = Required.Always)]
        public string Tag { get; set; }

        [JsonProperty(PropertyName = "levels", Required = Required.Always)]
        public List<object> Levels { get; set; }
    }
}
