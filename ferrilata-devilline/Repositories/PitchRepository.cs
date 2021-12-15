using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ferrilata_devilline.Models.DAOs;
using Microsoft.EntityFrameworkCore;

namespace ferrilata_devilline.Repositories
{
    public class PitchRepository : IPitchRepository
    {
        private readonly ApplicationContext _applicationContext;

        public PitchRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public List<Pitch> RetrievePitchesFromDB()
        {
            return  _applicationContext.Pitches
                .Include(pitch => pitch.Level)
                .ThenInclude(level => level.Badge)
                .Include(pitch => pitch.User)
                .ToList();
        }

        public Pitch FindPitchById(long id)
        {
            return RetrievePitchesFromDB().SingleOrDefault(x => x.PitchId == id);
        }

        public void SavePitch(Pitch pitch)
        {
            _applicationContext.Add(pitch);
            _applicationContext.SaveChanges();
        }
        public void Update()
        {
            _applicationContext.SaveChanges();
        }

        public void Delete(Pitch pitch)
        {
            _applicationContext.Remove(pitch);
        }
    }
}
