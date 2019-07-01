using System;
using System.Collections.Generic;

namespace ferrilata_devilline.Models
{
    public class Pitch
    {
        public string BadgeName { get; set; }
        public long OldLVL { get; set; }
        public long PitchedLVL { get; set; }
        public string PitchMessage { get; set; }
        public List<string> Holders { get; set; }
    }
}
