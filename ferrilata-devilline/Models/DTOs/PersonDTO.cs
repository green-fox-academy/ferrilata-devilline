using Newtonsoft.Json;

namespace ferrilata_devilline.Models.DTOs
{
    public class PersonDTO
    {
        [JsonProperty(PropertyName = "id")]
        public long PersonId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}