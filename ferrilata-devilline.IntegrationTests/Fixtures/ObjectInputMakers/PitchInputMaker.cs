using ferrilata_devilline.IntegrationTests.Fixtures.Models;
using ferrilata_devilline.Models;
using ferrilata_devilline.Models.DTOs;
using ferrilata_devilline.Models.DTOs.In;
using ferrilata_devilline.Models.DTOs.Input;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace ferrilata_devilline.IntegrationTests.Fixtures.ObjectInputMakers
{
    public static class PitchInputMaker
    {
        public static PitchInDTO MakeCorrectPitchInDTO()
        {
            return new PitchInDTO
            {
                Status = "status",
                PitchedMessage = "message",
                PitchedLevel = "3",
                Result = "result",
                Created = 21212,
                User = MakeNewUser(),
                Level = MakeNewLevel(),
                Reviews = MakeNewReviews()
            };
        }

        public static PitchInDTOWithNullValue MakeInCorrectPitchInDTO()
        {
            return new PitchInDTOWithNullValue
            {
                Status = "status",
                PitchMessage = null,
                PitchedLevel = 3,
                Result = "result",
                Created = 121212,
                User = MakeNewUser(),
                Level = MakeNewLevel(),
                Reviews = MakeNewReviews()
            };
        }

        public static PitchDTO MakeCorrectPitchDTO()
        {
            return  new PitchDTO
            {
                PitchId = 1,
                Status = "status",
                PitchedMessage = "message",
                PitchedLevel = "3",
                Result = "result updated",
                Created = 5,
                User = MakeNewUser(),
                Level = MakeNewLevel(),
                Reviews = MakeNewReviews()
            }; 
        }

        private static UserDTO MakeNewUser()
        {
            return new UserDTO
            {
                UserId = 1,
                Name = "name"
            };
        }

        private static LevelMiniDTO MakeNewLevel()
        {
            return new LevelMiniDTO
            {
                LevelId = 1,
                LevelNumber = 8
            };
        }

        private static List<ReviewDTO> MakeNewReviews()
        {
            var newReviewer = new ReviewerDTO
            {
                ReviewerId = 1,
                Name = "reviewerName"
            };

            return new List<ReviewDTO>
            {
                new ReviewDTO
                {
                    ReviewId = 1,
                    Message = "message",
                    Result = "result",
                    Reviewer = newReviewer
                }
            };
        }
    }
}