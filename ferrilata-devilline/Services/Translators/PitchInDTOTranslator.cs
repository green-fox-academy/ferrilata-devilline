using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Models.DTOs;
using ferrilata_devilline.Repositories;
using System.Linq;

namespace ferrilata_devilline.Services.Translators
{
    public class PitchInDTOTranslator
    {
        readonly ApplicationContext _context;

        public PitchInDTOTranslator(ApplicationContext context)
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
    }
}
