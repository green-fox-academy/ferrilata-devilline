using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ferrilata_devilline.Models
{
    [Serializable]
    public class Pitch
    {
        
        public int Id { get; set; }
        [Required]
        public DateTime Timestamp { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [field:NonSerialized]
        public string BadgeName { get; set; }
        [Required]
        public int OldLevel { get; set; }
        [Required]
        public int PitchedLevel { get; set; }
        [Required]
        public string PitchMessage { get; set; }
        [Required]
        public List<Reviewer> Reviewers { get; set; }
        [Required]
        public string Status { get; set; }
    }
}