﻿using Newtonsoft.Json;

namespace ferrilata_devilline.Models.DTOs
{
    public class ReviewDTO
    {
        [JsonProperty(PropertyName = "reviewId", Required = Required.Always)]
        public long ReviewId { get; set; }

        [JsonProperty(PropertyName = "message", Required = Required.Always)]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "result", Required = Required.Always)]
        public string Result { get; set; }

        [JsonProperty(PropertyName = "reviewer", Required = Required.Always)]
        public ReviewerDTO Reviewer { get; set; }
    }
}