using ferrilata_devilline.Models.DTOs.In;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ferrilata_devilline.Models.DTOs.Input
{
    public class PitchDTO
    {
        [JsonProperty(PropertyName = "id", Required = Required.Always)]
        public long PitchId { get; set; }

        [JsonProperty(PropertyName = "status", Required = Required.Always)]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "pitchedMessage", Required = Required.Always)]
        public string PitchedMessage { get; set; }

        [JsonProperty(PropertyName = "pitchedLevel", Required = Required.Always)]
        public string PitchedLevel { get; set; }

        [JsonProperty(PropertyName = "result", Required = Required.Always)]
        public string Result;

        [JsonProperty(PropertyName = "created", Required = Required.Always)]
        public long Created;

        [JsonProperty(PropertyName = "user", Required = Required.Always)]
        public UserDTO User;

        [JsonProperty(PropertyName = "level", Required = Required.Always)]
        public LevelMiniDTO Level;

        [JsonProperty(PropertyName = "reviews", Required = Required.Always)]
        public List<ReviewDTO> Reviews;
    }
}
