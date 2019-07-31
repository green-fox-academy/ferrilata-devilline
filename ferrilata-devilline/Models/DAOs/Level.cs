using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ferrilata_devilline.Models.DAOs
{
    public class Level
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long LevelId { get; set; }
        public int LevelNumber { get; set; }
        public string Description { get; set; }
        public string Weight { get; set; }
        public Badge Badge { get; set; }
        public List<UserLevel> UserLevels { get; set; }
        public List<Pitch> Pitches { get; set; }
    }
}
