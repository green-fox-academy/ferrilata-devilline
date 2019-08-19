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

        protected bool Equals(Level other)
        {
            return LevelId == other.LevelId && LevelNumber == other.LevelNumber &&
                   string.Equals(Description, other.Description) && string.Equals(Weight, other.Weight) &&
                   Equals(Badge,
                       other.Badge) && Equals(UserLevels, other.UserLevels) && Equals(Pitches, other.Pitches);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Level) obj);
        }

        public bool EqualsIgnoringBadge(Level other)
        {
            return LevelId == other.LevelId && LevelNumber == other.LevelNumber &&
                   string.Equals(Description, other.Description) && string.Equals(Weight, other.Weight) &&
                   Equals(UserLevels, other.UserLevels) && Equals(Pitches, other.Pitches);
        }
    }
}