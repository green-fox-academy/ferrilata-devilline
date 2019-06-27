using System.Collections.Generic;

namespace ferrilata_devilline.Models
{
    public class Pitches
    {
        public List<Pitch> myPitches;
        public List<Pitch> pitchesToReview;

        public Pitches()
        {
            myPitches = new List<Pitch>
            {
                new Pitch("balazs.barna", "Programming", 2, 3, "I improved in React, Redux, basic JS, " +
                                                               "NodeJS, Express and in LowDB, pls give me more money")
            };
            pitchesToReview = new List<Pitch>
            {
                new Pitch("berei.daniel", "English speaker", 2, 3, "I was working abroad for six years," +
                                                                   " so I can speak english very well. Pls improve my badge level to 3.")
            };
        }
    }
}