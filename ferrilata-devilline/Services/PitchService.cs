using AutoMapper;
using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Models.DTOs;
using ferrilata_devilline.Repositories;
using ferrilata_devilline.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ferrilata_devilline.Services
{
    public class PitchService : IPitchService
    {
        private readonly IPitchRepository _pitchRepository;
        private readonly IMapper _mapper;

        public PitchService(IPitchRepository pitchRepository, IMapper mapper)
        {
            _pitchRepository = pitchRepository;
            _mapper = mapper;
        }

        public Pitch GetPitchFromPitchInDTO(PitchInDTO IncomingPicth)
        {
            Pitch pitchToSave = _mapper.Map<PitchInDTO, Pitch>(IncomingPicth);
            return pitchToSave;
        }

        public void Delete(Pitch pitch)
        {
            _pitchRepository.Delete(pitch);
        }

        public Pitch FindPitchById(long id)
        {
            return _pitchRepository.FindPitchById(id);
        }

        public List<Pitch> GetAll()
        {
            return _pitchRepository.RetrievePitchesFromDB();
        }

        public void Update()
        {
            _pitchRepository.Update();
        }

        public void SavePitchFromPitchInDTO(long levelid, User user, PitchInDTO PitchDTO)
        {
            Pitch pitchToSave = _mapper.Map<PitchInDTO, Pitch>(PitchDTO);
            pitchToSave.User = user;
            pitchToSave.PitchedLevel = levelid.ToString();
            _pitchRepository.SavePitch(pitchToSave);
        }
    }
}
