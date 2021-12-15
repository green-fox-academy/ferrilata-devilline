using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ferrilata_devilline.Services.Interfaces
{
    public interface IPitchService
    {
        Pitch FindPitchById(long id);
        List<Pitch> GetAll();
        Pitch GetPitchFromPitchInDTO(PitchInDTO IncomingPicth);
        void SavePitchFromPitchInDTO(long levelid, User user, User reviewer, PitchInDTO pitchDTO);
        void Delete(Pitch pitch);
        void Update();
    }
}
