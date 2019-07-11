using ferrilata_devilline.Models;
using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Repositories;
using ferrilata_devilline.Services.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
            return new Pitches
            {
                MyPitches = _context.Pitches
                                .Include("User")
                                .Include("Level")
                                .Where(p => p.User.Email.Equals(userEmail))
                                .ToList(),

                PitchesToReview = _context.Reviews
                                .Include("User")
                                .Include("Pitch")
                                .Where(u => u.User.Email.Equals(userEmail))
                                .Select(r => r.Pitch)
                                .Include("User")
                                .Include("Level")
                                .ToList(),
            };
        }

        public void Save(Pitch pitch)
        {
            _context.Pitches.Add(pitch);
            _context.SaveChanges();
        }
    }
}