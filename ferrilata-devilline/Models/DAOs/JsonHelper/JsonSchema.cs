using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ferrilata_devilline.Models.DAOs.JsonHelper
{
    public class JsonSchema
    {
        [Key]
        public string Class { get; set; }

        public string Schema { get; set; }
    }
}
