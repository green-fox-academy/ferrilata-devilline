using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ferrilata_devilline.Models.DAOs
{
    public class Pitch
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonProperty(PropertyName = "pitchId")]
        public long PitchId { get; set; }

        [JsonProperty(PropertyName = "status")]
        public bool Status { get; set; }

        [JsonProperty(PropertyName = "pitchedLevel")]
        public string PitchedLevel { get; set; }

        [JsonProperty(PropertyName = "pitchedMessage")]
        public string PitchedMessage { get; set; }

        [JsonProperty(PropertyName = "result")]
        public string Result { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonProperty(PropertyName = "created")]
        public DateTime Created { get; set; }

        [JsonProperty(PropertyName = "user")]
        public User User { get; set; }

        [JsonProperty(PropertyName = "level")]
        public Level Level { get; set; }
    }
}