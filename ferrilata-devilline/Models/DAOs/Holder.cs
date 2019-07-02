using Newtonsoft.Json;

namespace ferrilata_devilline.Models.DAOs
{
    public class Holder
    {
        [JsonProperty(PropertyName = "id")]
        public long Id { set; get; }

        [JsonProperty(PropertyName = "name")]
        public string Name { set; get; }
    }
}
