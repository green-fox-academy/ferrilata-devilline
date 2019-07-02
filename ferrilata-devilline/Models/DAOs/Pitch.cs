
using System;
using System.Collections.Generic;

namespace ferrilata_devilline.Models
{
    public class Pitch
    {
        public DateTime Timestamp { get; set; }
        public string Username { get; set; }
        public string BadgeName { get; set; }
        public int OldLevel { get; set; }
        public int PitchedLevel { get; set; }
        public string PitchMessage { get; set; }
        public List<Holder> Holders { get; set; }
    }
}

