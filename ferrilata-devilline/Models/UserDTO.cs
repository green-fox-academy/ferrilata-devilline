using Newtonsoft.Json;

namespace ferrilata_devilline.Models.DTOs
{
    public class UserDTO
    {
        [JsonProperty(PropertyName = "userId")]
        public long UserId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}
