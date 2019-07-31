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
                   Equals(Badge, other.Badge) && Equals(UserLevels, other.UserLevels) && Equals(Pitches, other.Pitches);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Level) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = LevelId.GetHashCode();
                hashCode = (hashCode * 397) ^ LevelNumber;
                hashCode = (hashCode * 397) ^ (Description != null ? Description.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Weight != null ? Weight.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Badge != null ? Badge.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (UserLevels != null ? UserLevels.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Pitches != null ? Pitches.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(Level left, Level right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Level left, Level right)
        {
            return !Equals(left, right);
        }
    }
}