using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Repositories;
using ferrilata_devilline.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ferrilata_devilline.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User FindById(long id)
        {
            return _userRepository.FindUserById(id);
        }
        public void Add(User user)
        {
            _userRepository.SaveUser(user);
        }

        public void DeleteById(long id)
        {
            _userRepository.DeleteUsereById(id);
        }

        public List<User> GetAll()
        {
            return _userRepository.RetrieveUsersFromDB();
        }

        public void Update()
        {
            _userRepository.Update();
        }

        public User FindByEmail(string email)
        {
            return _userRepository.RetrieveUsersFromDB().SingleOrDefault(x => x.Email == email);
        }

        public bool IsNewUser(string email)
        {
            return FindByEmail(email) == null ? true : false;
        }

        public bool IsThereLevelFromSameBadge(long badgeId, User user)
        {
            if (user.UserLevels == null)
            {
                return false;
            }

            bool istehere = user.UserLevels.FirstOrDefault(x => x.Level.Badge.BadgeId == badgeId) == null ? false : true;
            return istehere;
        }

        public Level GetLevelFromSameBadge(long badgeId, User user)
        {
            Level levelToReturn = user.UserLevels.FirstOrDefault(x => x.Level.Badge.BadgeId == badgeId).Level;
            return levelToReturn;
        }
    }
}
