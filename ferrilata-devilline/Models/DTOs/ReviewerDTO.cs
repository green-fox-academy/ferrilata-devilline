using Newtonsoft.Json;

namespace ferrilata_devilline.Models.DTOs
{ 
    public class ReviewerDTO
    {
        [JsonProperty(PropertyName = "reviewerId", Required = Required.Always)]
        public long ReviewerId { get; set; }

        [JsonProperty(PropertyName = "name", Required = Required.Always)]
        public string Name { get; set; }
    }
}
