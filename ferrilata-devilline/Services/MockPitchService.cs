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
                    new Pitch("balazs.barna", "Programming", 2, 3, "I improved in React, Redux, basic JS, " +
                                                               "NodeJS, Express and in LowDB, pls give me more money")
                },

                PitchesToReview = new List<Pitch>
                {
                    new Pitch("berei.daniel", "English speaker", 2, 3, "I was working abroad for six years," +
                                                                   " so I can speak english very well. Pls improve my badge level to 3.")
                }
            };
        }
    }
}
