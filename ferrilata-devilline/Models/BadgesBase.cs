using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ferrilata_devilline.Models
{
    public class BadgesBase
    {
        public List<Badge> Badges { get; set; }
        public BadgesBase()
        {
            Badges = new List<Badge>();
            Badges.Add(new Badge { Name = "Process improve", Level = 2 });
            Badges.Add(new Badge { Name = "English speaker", Level = 1 });
            Badges.Add(new Badge { Name = "Feedback giver", Level = 1 });
        }
    }
}
