using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ferrilata_devilline.Models.DTOs.In
{
    public class LevelInMiniDTO
    {
        [JsonProperty(PropertyName = "levelId", Required = Required.Always)]
        public long LevelId { get; set; }

        [JsonProperty(PropertyName = "level", Required = Required.Always)]
        public int LevelNumber { get; set; }

    }
}
