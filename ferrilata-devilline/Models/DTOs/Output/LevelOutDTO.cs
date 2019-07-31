using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

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

        public override bool Equals(object obj)
        {
            return obj is LevelOutDTO dTO &&
                   LevelId == dTO.LevelId &&
                   LevelNumber == dTO.LevelNumber &&
                   Weight == dTO.Weight &&
                   Description == dTO.Description &&
                   CompareHolders(dTO.Holders);
        }

        public bool CompareHolders(List<PersonDTO> list)
        {
            for (int i = 0; i < Holders.Count(); i++)
            {
                if (!Holders[i].Equals(list[i]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}