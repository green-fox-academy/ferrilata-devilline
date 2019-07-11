using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ferrilata_devilline.Models.DTOs
{
    public class HolderDTO
    {
        [JsonProperty(PropertyName = "id")]
        public long HolderId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}
