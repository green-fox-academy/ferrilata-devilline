using System.ComponentModel.DataAnnotations;

namespace ferrilata_devilline.Models.DAOs.JsonHelper
{
    public class JsonSchema
    {
        [Key]
        public string Class { get; set; }

        public string Schema { get; set; }
    }
}
