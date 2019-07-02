using System.Collections.Generic;

namespace ferrilata_devilline.Models.DAOs
{
    public class Badge
    {
        public long Id { get; set; }
        public double Version { get; set; }
        public string Name { get; set; }
        public string Tag { get; set; }
        public List<Level> Levels { get; set; }
    }
}
