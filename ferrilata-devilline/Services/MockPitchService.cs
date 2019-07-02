using ferrilata_devilline.Models;
using ferrilata_devilline.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ferrilata_devilline.Services
{
    public class MockPitchService : IPitchService
    {
        public Pitches GetPitches()
        {
            return GeneratePitches();
        }

        private Pitches GeneratePitches()
        {
            return new Pitches
            {
                MyPitches = new List<Pitch>
                {
                    new Pitch { Id = 1, Timestamp = DateTime.Now, Username = "balazs.barna",  BadgeName = "Programming", 
                                OldLevel = 2, PitchedLevel = 3,
                                PitchMessage = "I improved in React, Redux, basic JS, NodeJS, Express and in LowDB, pls give me more money", 
                                Reviewers = new List<Reviewer>
                                                          {
                                                             new Reviewer {  Name = "sandor.vass", Message = null, PitchStatus = false }
                                                          }
                                }
                },

                PitchesToReview = new List<Pitch>
                {
                    new Pitch { Id = 2, Timestamp = DateTime.Now, Username = "balazs.barna",  BadgeName = "Programming",
                                OldLevel = 2, PitchedLevel = 3,
                                PitchMessage = "I improved in React, Redux, basic JS, NodeJS, Express and in LowDB, pls give me more money",
                                Reviewers = new List<Reviewer>
                                                          {
                                                             new Reviewer {  Name = "sandor.vass", Message = null, PitchStatus = false }
                                                          }
                               }
                }
            };
        }
    }
}
