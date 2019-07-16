using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ferrilata_devilline.Models.DTOs
{
    public class LevelInDTO
    {
        [Required]
        public int LevelNumber { get; set; }

        [Required]
        public string Weight { get; set; }

        [Required]
        public string Description { get; set; }
    }
}