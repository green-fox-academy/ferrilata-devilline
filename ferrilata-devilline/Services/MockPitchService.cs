using ferrilata_devilline.Models.DTOs;
using ferrilata_devilline.Services.Interfaces;
using System.Collections.Generic;

namespace ferrilata_devilline.Services
{
    public class MockPitchService : IPitchService
    {
        public Pitches GetPitches()
        {
            return new Pitches
            {
                MyPitches = new List<PitchDTO> { },
                PitchesToReview = new List<PitchDTO> { }
            };
        }
    }
}