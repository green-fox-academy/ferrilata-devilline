using ferrilata_devilline.Models.DTOs;
using ferrilata_devilline.Models.DTOs.In;
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
        public DateTime Created;

        [JsonProperty(PropertyName = "user")]
        public PersonDTO User;

        [JsonProperty(PropertyName = "level")]
        public LevelInMiniDTO Level;

        [JsonProperty(PropertyName = "reviews")]
        public List<ReviewDTO> Reviews;
    }
}
