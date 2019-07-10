using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ferrilata_devilline.Models
{
    public class AuxPitch
    {
        [Key] public long Id { get; set; }
        public string BadgeName { get; set; }
        public long OldLVL { get; set; }
        public long PitchedLVL { get; set; }
        public string PitchMessage { get; set; }
        [NotMapped] public IEnumerable<string> Holders { get; set; }
    }
}