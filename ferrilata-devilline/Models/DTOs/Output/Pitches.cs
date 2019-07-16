using Newtonsoft.Json;
using System.Collections.Generic;

namespace ferrilata_devilline.Models.DTOs
{
    public class Pitches
    {
        [JsonProperty(PropertyName = "myPitches")]
        public List<PitchDTO> MyPitches { get; set; }

        [JsonProperty(PropertyName = "pitchesToReview")]
        public List<PitchDTO> PitchesToReview { get; set; }
    }
}