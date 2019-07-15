using ferrilata_devilline.Models.DTOs;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ferrilata_devilline.Services.Helpers.Extensions.ObjectTypeCheckers.Models
{
    public class PitchInDTOWithNullValues
    {
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "pitchedMessage")]
        public string PitchMessage { get; set; }

        [JsonProperty(PropertyName = "pitchedLevel")]
        public int PitchedLevel { get; set; }

        [JsonProperty(PropertyName = "result")]
        public string Result { get; set; }

        [JsonProperty(PropertyName = "created")]
        public long Created { get; set; }

        [JsonProperty(PropertyName = "user")]
        public UserDTO User { get; set; }

        [JsonProperty(PropertyName = "level")]
        public LevelMiniDTO Level { get; set; }

        [JsonProperty(PropertyName = "reviews")]
        public List<ReviewDTO> Reviews { get; set; }
    }
}
