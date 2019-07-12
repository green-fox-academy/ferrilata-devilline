using ferrilata_devilline.Models;
using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Repositories;
using ferrilata_devilline.Services.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using ferrilata_devilline.Models.DTOs;
using ferrilata_devilline.Services.Translators;
using ferrilata_devilline.Models.DTOs.Input;

namespace ferrilata_devilline.Services
{
    public class PitchService : IPitchService
    {
        readonly ApplicationContext _context;

        public PitchService(ApplicationContext context)
        {
            _context = context;
        }

        public Pitches GetPitches(string userEmail)
        {
            var Translator = new PitchTranslator(_context);

            var myPitches = new List<PitchDTO> {  };
            _context.Pitches
                            .Include("User")
                            .Include("Level")
                            .Where(p => p.User.Email.Equals(userEmail))
                            .ToList()
                            .ForEach(p => myPitches.Add(Translator.TranslateToPitchDTO(p)));

            var pitchesToReview = new List<PitchDTO> { };
            _context.Reviews
                            .Include("User")
                            .Include("Pitch")
                            .Where(r => r.User.Email.Equals(userEmail))
                            .Select(r => r.Pitch)
                            .Include("User")
                            .Include("Level")
                            .ToList()
                            .ForEach(p => pitchesToReview.Add(Translator.TranslateToPitchDTO(p)));

            return new Pitches
            {
                MyPitches = myPitches,
                PitchesToReview = pitchesToReview
            };
        }

        public void Save(Pitch pitch)
        {
            _context.Pitches.Add(pitch);
            _context.SaveChanges();
        }

        public void Update(Pitch newPitch)
        {
            var oldPitch = _context.Pitches.Where(p => p.PitchId == newPitch.PitchId).FirstOrDefault();
            _context.Pitches.Remove(oldPitch);
            _context.SaveChanges();
            Save(newPitch);
        }

        public void TranslateAndSave(JToken requestBody)
        {
            var Translator = new PitchTranslator(_context);
            var incomingPitch = requestBody.ToObject<PitchInDTO>();

            var newPitch = Translator.TranslateToPitch(incomingPitch);

            Save(newPitch);
        }

        public void TranslateAndUpdate(JToken requestBody)
        {
            var Translator = new PitchTranslator(_context);
            var incomingPitch = requestBody.ToObject<PitchDTO>();

            var updatablePitch = Translator.TranslateToPitch(incomingPitch);

            Update(updatablePitch);
        }
    }
}