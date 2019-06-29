using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ferrilata_devilline.Models.DTOs
{
    public class AdminDTO
    {
        public string version { get; set; }
        public string name { get; set; }
        public string tag { get; set; }
        public List<object> levels { get; set; }
    }
}
