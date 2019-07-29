using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ferrilata_devilline.Models
{
    public class AuxPitch
    {
        [Key] public long Id { get; set; }

        [Required]
        public string BadgeName { get; set; }

        [Required]
        public long OldLVL { get; set; }

        [Required]
        public long PitchedLVL { get; set; }

        [Required]
        public string PitchMessage { get; set; }

        [Required]
        [NotMapped] public IEnumerable<string> Holders { get; set; }
    }
}