using ferrilata_devilline.Models.DAOs;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ferrilata_devilline.Models.DTOs
{
    public class PitchInDTO
    {
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; } = "open";

        [JsonProperty(PropertyName = "pitchedMessage")]
        [Required]
        public string PitchedMessage { get; set; }

        //[JsonProperty(PropertyName = "pitchedLevel")]
        //public string PitchedLevel { get; set; }

        //[JsonProperty(PropertyName = "result")]
        //public string Result { get; set; }

        //[JsonProperty(PropertyName = "created", Required = Required.Always)]
        //public long Created { get; set; }

        //[JsonProperty(PropertyName = "user", Required = Required.Always)]
        //public UserDTO User { get; set; }

        [JsonProperty(PropertyName = "level")]
        public Level Level { get; set; }

        [JsonProperty(PropertyName = "reviews")]
        public List<ReviewDTO> Reviews { get; set; }
    }
}