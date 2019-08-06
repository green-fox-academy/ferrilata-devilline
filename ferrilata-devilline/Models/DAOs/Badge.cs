using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

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

        public List<Level> Levels { get; set; }

        protected bool Equals(Badge other)
        {
            var nonLevelFieldsEqual = BadgeId == other.BadgeId && Math.Abs(Version - other.Version) < 0.001 &&
                                      string.Equals(Name, other.Name) && string.Equals(Tag, other.Tag);

            var levelsEqual = Levels
                .Select(l => new {l.Description, l.Pitches, l.Weight, l.LevelId, l.LevelNumber, l.UserLevels})
                .SequenceEqual(other.Levels.Select(l => new
                    {l.Description, l.Pitches, l.Weight, l.LevelId, l.LevelNumber, l.UserLevels}));
            return nonLevelFieldsEqual && levelsEqual;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Badge) obj);
        }
    }
}