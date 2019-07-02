using System.Collections.Generic;
using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Services.Interfaces;

namespace ferrilata_devilline.Services
{
    public class MockBadgeService : IBadgeService
    {
        public List<Badge> GetAll()
        {
            Holder holder1 = new Holder { Id = 45, Name = "balazs.barna" };
            Holder holder2 = new Holder { Id = 21, Name = "some.holder.name" };
            string level1Description = "I can see through processes and propose relevant " +
                "and doable ideas for improvement.I can create improved definition / " +
                "accountibility / documentation and communicate it to the team";

            Badge badge1 = new Badge
            {
                Id = 3,
                Version = 2.3,
                Name = "Process improver",
                Tag = "general",
                Levels = new List<Level>
                    {
                    new Level { Id = 12, LevelNumber = 1,
                        Weight = 2, Description = level1Description,
                        Holders = new List<Holder>
                        {
                            holder1,
                            holder2
                        }
                    } ,
                    new Level { Id = 8, LevelNumber = 3,
                        Weight = 4, Description = "such level description",
                        Holders = new List<Holder>
                        {
                            holder2
                        }
                    }
                }
            };

            Badge badge2 = new Badge
            {
                Id = 222,
                Version = 1.1,
                Name = "another badge",
                Tag = "specific",
                Levels = new List<Level>
                    {
                    new Level { Id = 5, LevelNumber = 6,
                        Weight = 13, Description = "another level description",
                        Holders = new List<Holder>
                        {
                            holder1
                        }
                    } 
                }
            };

            return new List<Badge> { badge1, badge2 };
        }
    }
}
