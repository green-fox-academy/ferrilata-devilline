using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ferrilata_devilline.Models.DAOs
{
    public class Pitch
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PitchId { get; set; }

        public string Status { get; set; }

        public string PitchedLevel { get; set; }

        public string PitchedMessage { get; set; }

        public string Result { get; set; }

        public long Created { get; set; }

        public User User { get; set; }

        public Level Level { get; set; }

        public List<Review> Reviews { get; set; }

        public Pitch()
        {
            Created = DateTime.Now.Millisecond;
        }
    }
}