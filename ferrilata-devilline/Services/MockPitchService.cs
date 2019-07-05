using ferrilata_devilline.Models;
using ferrilata_devilline.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace ferrilata_devilline.Services
{
    public class MockPitchService : IPitchService
    {
        public Pitches GetPitches()
        {
            Reviewer reviewer1 = new Reviewer
                {
                    Name = "sandor.vass",
                    Message = null,
                    PitchStatus = false
                };

            Pitch myPitch1 = new Pitch
                {
                    Id = 1,
                    Timestamp = DateTime.Now,
                    Username = "balazs.barna",
                    BadgeName = "Programming",
                    OldLevel = 2,
                    PitchedLevel = 3,
                    PitchMessage = "I improved in React, Redux, basic JS, NodeJS, Express and in LowDB, " +
                            "pls give me more money",
                    Reviewers = new List<Reviewer> { reviewer1 }
                };

            Reviewer reviewer2 = new Reviewer
                {
                    Name = "not.sandor.vass",
                    Message = "here is a message",
                    PitchStatus = true
                };

            Pitch pitchToReview1 = new Pitch
            {
                Id = 2,
                Timestamp = DateTime.Now,
                Username = "not.balazs.barna",
                BadgeName = "Programming 2",
                OldLevel = 4,
                PitchedLevel = 5,
                PitchMessage = "I improved in other programming languages",
                Reviewers = new List<Reviewer> { reviewer2 }
            };

            return new Pitches
            {
                MyPitches = new List<Pitch> { myPitch1 },
                PitchesToReview = new List<Pitch> { pitchToReview1 }
            };
        }
    }
}