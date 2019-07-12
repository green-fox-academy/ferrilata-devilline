using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ferrilata_devilline.Models.DAOs
{
    public class Badge
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long BadgeId { get; set; }

        public double Version { get; set; }

        public string Name { get; set; }

        public string Tag { get; set; }
    }
}