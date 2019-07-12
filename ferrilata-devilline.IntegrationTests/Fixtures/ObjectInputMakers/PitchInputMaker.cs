using ferrilata_devilline.IntegrationTests.Fixtures.Models;
using ferrilata_devilline.Models;
using ferrilata_devilline.Models.DTOs;
using ferrilata_devilline.Models.DTOs.In;
using System;
using System.Collections.Generic;
using System.Text;

namespace ferrilata_devilline.IntegrationTests.Fixtures.ObjectInputMakers
{
    public static class PitchInputMaker
    {
        public static PitchInDTO MakeCorrect()
        {
            return new PitchInDTO
            {
                Status = "status",
                PitchMessage = "message",
                PitchedLevel = 3,
                Result = "result",
                Created = DateTime.Now,
                User = MakeNewUser(),
                Level = MakeNewLevel(),
                Reviews = MakeNewReviews()
            };
        }

        public static PitchInDTOWithNullValue MakeInCorrect()
        {
            return new PitchInDTOWithNullValue
            {
                Status = "status",
                PitchMessage = null,
                PitchedLevel = 3,
                Result = "result",
                Created = DateTime.Now,
                User = MakeNewUser(),
                Level = MakeNewLevel(),
                Reviews = MakeNewReviews()
            };
        }

        private static PersonDTO MakeNewUser()
        {
            return new PersonDTO
            {
                PersonId = 1,
                Name = "name"
            };
        }

        private static LevelInMiniDTO MakeNewLevel()
        {
            return new LevelInMiniDTO
            {
                LevelId = 2,
                LevelNumber = 8
            };
        }

        private static List<ReviewDTO> MakeNewReviews()
        {
            var newReviewer = new ReviewerDTO
            {
                ReviewerId = 22,
                Name = "reviewerName"
            };

            return new List<ReviewDTO>
            {
                new ReviewDTO
                {
                    ReviewId = 3,
                    Message = "message",
                    Result = "result",
                    Reviewer = newReviewer
                }
            };
        }
    }
}