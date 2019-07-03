
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
        public string Status { get; set; }

        public Pitch(string username, string badgeName, int oldLevel, int pitchedLevel, string pitchMessage)
        {
            Username = username;
            Timestamp = DateTime.Now;
            BadgeName = badgeName;
            OldLevel = oldLevel;
            PitchedLevel = pitchedLevel;
            PitchMessage = pitchMessage;
            Holders = new List<Holder>
            {
                new Holder("sandor.vass", null, false)
            };
        }
    }
}

