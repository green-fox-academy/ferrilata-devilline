using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ferrilata_devilline.Models.DTOs
{
    public class PitchDTO
    {
        [Required]
        public long PitchId { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public string PitchedMessage { get; set; }

        [Required]
        public string PitchedLevel { get; set; }

        [Required]
        public string Result { get; set; }

        [Required]
        public long Created { get; set; }

        [Required]
        public UserDTO User { get; set; }

        [Required]
        public LevelMiniDTO Level { get; set; }

        [Required]
        public List<ReviewDTO> Reviews { get; set; }
    }
}
