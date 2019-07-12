using ferrilata_devilline.Models.DTOs;
using ferrilata_devilline.Models.DTOs.In;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ferrilata_devilline.Models
{
    public class PitchInDTO
    {
        [JsonProperty(PropertyName = "status", Required = Required.Always)]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "pitchedMessage", Required = Required.Always)]
        public string PitchMessage { get; set; }

        [JsonProperty(PropertyName = "pitchedLevel", Required = Required.Always)]
        public int PitchedLevel { get; set; }

        [JsonProperty(PropertyName = "result", Required = Required.Always)]
        public string Result;

        [JsonProperty(PropertyName = "created", Required = Required.Always)]
        public DateTime Created;

        [JsonProperty(PropertyName = "user", Required = Required.Always)]
        public PersonDTO User;

        [JsonProperty(PropertyName = "level", Required = Required.Always)]
        public LevelInMiniDTO Level;

        [JsonProperty(PropertyName = "reviews", Required = Required.Always)]
        public List<ReviewDTO> Reviews;
    }
}



