﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace ferrilata_devilline.Models.DTOs
{
    public class LevelOutDTO
    {
        [JsonProperty(PropertyName = "id")]
        public long LevelId { get; set; }

        [JsonProperty(PropertyName = "level")]
        public long LevelNumber { get; set; }

        [JsonProperty(PropertyName = "weight")]
        public string Weight { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "holders")]
        public List<PersonDTO> Holders { get; set; }
    }
}