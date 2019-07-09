using ferrilata_devilline.Models;
using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace ferrilata_devilline.Services
{
    public class MockPitchService : IPitchService
    {
        public Pitches GetPitches()
        {
            return new Pitches
            {
                MyPitches = new List<Pitch> { },
                PitchesToReview = new List<Pitch> {  }
            };
        }
    }
}