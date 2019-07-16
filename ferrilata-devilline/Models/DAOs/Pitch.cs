using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ferrilata_devilline.Models.DAOs
{
    public class Pitch
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public long PitchId { get; set; }

        [DefaultValue(false)]
        [Required]
        public string Status { get; set; }

        [Required]
        public string PitchedLevel { get; set; }

        [Required]
        public string PitchedMessage { get; set; }

        [Required]
        public string Result { get; set; }

        public long Created { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public Level Level { get; set; }

        
        public Pitch()
        {
            Created = DateTime.Now.Millisecond;
        }
    }
}