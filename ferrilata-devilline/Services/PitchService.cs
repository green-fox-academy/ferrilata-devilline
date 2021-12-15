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
        private readonly ILevelService _levelService;
        private readonly IReviewService _reviewService;

        public PitchService(IPitchRepository pitchRepository, IMapper mapper, ILevelService levelService, IReviewService reviewService)
        {
            _pitchRepository = pitchRepository;
            _mapper = mapper;
            _levelService = levelService;
            _reviewService = reviewService;
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

        public void SavePitchFromPitchInDTO(long levelid, User user, User reviewer, PitchInDTO PitchDTO)
        {
            Pitch pitchToSave = _mapper.Map<PitchInDTO, Pitch>(PitchDTO);
            pitchToSave.User = user;
            pitchToSave.Level = _levelService.FindLevelById(levelid);
            Review review = new Review { User = reviewer};
            pitchToSave.Reviews.Add(review);
            _pitchRepository.SavePitch(pitchToSave);
        }
    }
}
