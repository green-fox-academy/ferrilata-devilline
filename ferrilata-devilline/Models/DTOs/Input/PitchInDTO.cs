using Newtonsoft.Json;
using System.Collections.Generic;

namespace ferrilata_devilline.Models.DTOs
{
    public class PitchInDTO
    {
        [JsonProperty(PropertyName = "status", Required = Required.Always)]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "pitchedMessage", Required = Required.Always)]
        public string PitchedMessage { get; set; }

        [JsonProperty(PropertyName = "pitchedLevel", Required = Required.Always)]
        public string PitchedLevel { get; set; }

        [JsonProperty(PropertyName = "result", Required = Required.Always)]
        public string Result { get; set; }

        [JsonProperty(PropertyName = "created", Required = Required.Always)]
        public long Created { get; set; }

        [JsonProperty(PropertyName = "user", Required = Required.Always)]
        public UserDTO User { get; set; }

        [JsonProperty(PropertyName = "level", Required = Required.Always)]
        public LevelMiniDTO Level { get; set; }

        [JsonProperty(PropertyName = "reviews", Required = Required.Always)]
        public List<ReviewDTO> Reviews { get; set; }
    }
}