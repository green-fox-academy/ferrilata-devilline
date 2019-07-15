using System.ComponentModel.DataAnnotations;

namespace ferrilata_devilline.Models.DAOs.JsonHelper
{
    public class JsonSchemaForDevilline
    {
        [Key]
        public string Class { get; set; }

        public string Schema { get; set; }
    }
}
