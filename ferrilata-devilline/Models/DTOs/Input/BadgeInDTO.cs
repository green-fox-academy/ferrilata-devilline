using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ferrilata_devilline.Models.DTOs
{
    public class BadgeInDTO
    {
        [Required]
        public double Version { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Tag { get; set; }
        
        public List<LevelInDTO> Levels { get; set; }
    }
}