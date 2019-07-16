using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ferrilata_devilline.Models.DTOs
{
    public class PitchDTO
    {
        [Required]
        [JsonProperty(PropertyName = "id")]
        public long PitchId { get; set; }

        [JsonProperty(PropertyName = "status")]
        [Required]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "pitchedMessage")]
        [Required]
        public string PitchedMessage { get; set; }

        [JsonProperty(PropertyName = "pitchedLevel")]
        [Required]
        public string PitchedLevel { get; set; }

        [JsonProperty(PropertyName = "result")]
        [Required]
        public string Result { get; set; }

        [JsonProperty(PropertyName = "created")]
        [Required]
        public long Created { get; set; }

        [JsonProperty(PropertyName = "user")]
        [Required]
        public UserDTO User { get; set; }

        [JsonProperty(PropertyName = "level")]
        [Required]
        public LevelMiniDTO Level { get; set; }

        [JsonProperty(PropertyName = "reviews")]
        [Required]
        public List<ReviewDTO> Reviews { get; set; }
    }
}
