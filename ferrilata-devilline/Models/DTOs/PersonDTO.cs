﻿using Newtonsoft.Json;

namespace ferrilata_devilline.Models.DTOs
{
    public class PersonDTO
    {
        [JsonProperty(PropertyName = "id", Required = Required.Always)]
        public long PersonId { get; set; }

        [JsonProperty(PropertyName = "name", Required = Required.Always)]
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            return obj is PersonDTO dTO &&
                   PersonId == dTO.PersonId &&
                   Name == dTO.Name;
        }
    }
}