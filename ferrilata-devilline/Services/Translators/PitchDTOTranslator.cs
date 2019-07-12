using ferrilata_devilline.Models;
using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Models.DTOs;
using ferrilata_devilline.Models.DTOs.In;
using ferrilata_devilline.Models.DTOs.Input;
using ferrilata_devilline.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ferrilata_devilline.Services.Translators
{
    public class PitchTranslator
    {
        readonly ApplicationContext _context;

        public PitchTranslator(ApplicationContext context)
        {
            _context = context;
        }

        public Pitch TranslateToPitch(PitchInDTO incomingPitch)
        {
            return new Pitch
            {
                Status = incomingPitch.Status,
                PitchedLevel = incomingPitch.PitchedLevel,
                PitchedMessage = incomingPitch.PitchedMessage,
                Result = incomingPitch.Result,
                Created = incomingPitch.Created,
                User = _context.Users.Where(u => u.UserId == incomingPitch.User.UserId).FirstOrDefault(),
                Level = _context.Levels.Where(l => l.LevelId == incomingPitch.Level.LevelId).FirstOrDefault()
            };
        }

        public Pitch TranslateToPitch(PitchDTO incomingPitch)
        {
            return new Pitch
            {
                PitchId = incomingPitch.PitchId,
                Status = incomingPitch.Status,
                PitchedLevel = incomingPitch.PitchedLevel,
                PitchedMessage = incomingPitch.PitchedMessage,
                Result = incomingPitch.Result,
                Created = incomingPitch.Created,
                User = _context.Users.Where(u => u.UserId == incomingPitch.User.UserId).FirstOrDefault(),
                Level = _context.Levels.Where(l => l.LevelId == incomingPitch.Level.LevelId).FirstOrDefault()
            };
        }

        public PitchDTO TranslateToPitchDTO(Pitch originalPitch)
        {
            return new PitchDTO
            {
                PitchId = originalPitch.PitchId,
                Status = originalPitch.Status,
                PitchedMessage = originalPitch.PitchedMessage,
                PitchedLevel = originalPitch.PitchedLevel,
                Result = originalPitch.Result,
                Created = originalPitch.Created,
                User = TranslateToUserDTO(originalPitch.User),
                Level = TranslateToLevelMiniDTO(originalPitch.Level),
                Reviews = TranslateToReviewDTOList(_context.Reviews.Where(r => r.Pitch.PitchId == originalPitch.PitchId).Include("User").ToList())
            };
        }

        private UserDTO TranslateToUserDTO(User user)
        {
            return new UserDTO
            {
                UserId = user.UserId,
                Name = user.Name
            };
        }

        private LevelMiniDTO TranslateToLevelMiniDTO(Level level)
        {
            return new LevelMiniDTO
            {
                LevelId = level.LevelId,
                LevelNumber = level.LevelNumber
            };
        }

        private List<ReviewDTO> TranslateToReviewDTOList(List<Review> reviews)
        {
            List<ReviewDTO> result = new List<ReviewDTO> { };
            
            foreach (Review review in reviews)
            {
                ReviewDTO translated = new ReviewDTO
                {
                    ReviewId = review.ReviewId,
                    Message = review.Message,
                    Result = review.Result,
                    Reviewer = TranslateToReviewerDTO(review.User)
                };

                result.Add(translated);
            }

            return result;
        }

        private ReviewerDTO TranslateToReviewerDTO(User user)
        {
            return new ReviewerDTO
            {
                ReviewerId = user.UserId,
                Name = user.Name
            };
        }
    }
}
