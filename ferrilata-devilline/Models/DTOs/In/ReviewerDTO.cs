using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ferrilata_devilline.Models.DTOs.In
{
    public class ReviewerDTO
    {
        [JsonProperty(PropertyName = "reviewerID", Required = Required.Always)]
        public long ReviewerId { get; set; }

        [JsonProperty(PropertyName = "name", Required = Required.Always)]
        public string Name { get; set; }
    }
}
