using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ferrilata_devilline.Models.DTOs;
using Newtonsoft.Json;

namespace ferrilata_devilline.Models.DAOs
{
    public class BadgeOutDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonProperty(PropertyName = "id")]
        public long BadgeId { get; set; }

        public double Version { get; set; }

        public string Name { get; set; }

        public string Tag { get; set; }

        public List<LevelOutDTO> Levels { get; set; }
    }
}