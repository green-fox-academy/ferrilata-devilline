using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ferrilata_devilline.Models.DAOs;
using Microsoft.EntityFrameworkCore;

namespace ferrilata_devilline.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _applicationContext;

        public UserRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }
        public void Update()
        {
            _applicationContext.SaveChanges();
        }

        public List<User> RetrieveUsersFromDB()
        {
            return _applicationContext.Users
                .Include(user => user.Pitches)
                .ThenInclude(pitch => pitch.Level)
                .ThenInclude(level => level.Badge)
                .ToList();
        }

        public User FindUserById(long id)
        {
            return RetrieveUsersFromDB().SingleOrDefault(x => x.UserId == id);
        }

        public void DeleteUsereById(long id)
        {
            User userToDelete = FindUserById(id);
            _applicationContext.Users.Remove(userToDelete);
            _applicationContext.SaveChanges();
        }

        public void SaveUser(User user)
        {
            _applicationContext.Users.Add(user);
            _applicationContext.SaveChanges();
        }
    }
}
