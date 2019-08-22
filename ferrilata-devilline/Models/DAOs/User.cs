using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ferrilata_devilline.Models.DAOs
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Role { get; set; } = "User";

        public List<UserLevel> UserLevels { get; set; }

        public List<Pitch> Pitches { get; set; }

        public List<Review> Reviews { get; set; }
    }
}