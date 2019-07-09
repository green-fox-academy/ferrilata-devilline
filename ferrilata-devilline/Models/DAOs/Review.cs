
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ferrilata_devilline.Models.DAOs
{
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ReviewId { get; set; }

        public string Message { get; set; }

        public string Result { get; set; }

        public User User { get; set; }

        public Pitch Pitch { get; set; }
    }
}
