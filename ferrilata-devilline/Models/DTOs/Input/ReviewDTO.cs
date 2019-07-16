using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ferrilata_devilline.Models.DTOs
{
    public class ReviewDTO
    {
        [Required]
        public long ReviewId { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public string Result { get; set; }

        [Required]
        public ReviewerDTO Reviewer { get; set; }
    }
}
