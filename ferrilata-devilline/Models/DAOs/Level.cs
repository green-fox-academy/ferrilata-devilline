using System.Collections.Generic;

namespace ferrilata_devilline.Models.DAOs
{ 
    public class Level
    {
        public long Id { get; set; }
        public int LevelNumber { get; set; }
        public int Weight { get; set; }
        public string Description { get; set; }
        public List<Holder> Holders { get; set; }
    }
}
