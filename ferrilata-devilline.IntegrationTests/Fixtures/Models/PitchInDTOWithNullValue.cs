using ferrilata_devilline.Models.DTOs;
using ferrilata_devilline.Models.DTOs.In;
using ferrilata_devilline.Models.DTOs.Out;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ferrilata_devilline.IntegrationTests.Fixtures.Models
{
    public class PitchInDTOWithNullValue
    {
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "pitchedMessage")]
        public string PitchMessage { get; set; }

        [JsonProperty(PropertyName = "pitchedLevel")]
        public int PitchedLevel { get; set; }

        [JsonProperty(PropertyName = "result")]
        public string Result;

        [JsonProperty(PropertyName = "created")]
        public long Created;

        [JsonProperty(PropertyName = "user")]
        public UserDTO User;

        [JsonProperty(PropertyName = "level")]
        public LevelMiniDTO Level;

        [JsonProperty(PropertyName = "reviews")]
        public List<ReviewDTO> Reviews;
    }
}
