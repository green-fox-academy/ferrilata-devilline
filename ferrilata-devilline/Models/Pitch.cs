using System;
using System.Collections.Generic;

namespace ferrilata_devilline.Models
{
    public class Pitch
    {
        public DateTime timestamp { get; set; }
        public string username { get; set; }
        public string badgeName { get; set; }
        public int oldLevel { get; set; }
        public int pitchedLevel { get; set; }
        public string pitchMessage { get; set; }
        public List<Holder> holders { get; set; }

        public Pitch(string username, string badgeName, int oldLevel, int pitchedLevel, string pitchMessage)
        {
            this.username = username;
            timestamp = DateTime.Now;
            this.badgeName = badgeName;
            this.oldLevel = oldLevel;
            this.pitchedLevel = pitchedLevel;
            this.pitchMessage = pitchMessage;
            holders = new List<Holder>
            {
                new Holder("sandor.vass", null, false)
            };
        }
    }
}