using ferrilata_devilline.Models.DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ferrilata_devilline.Services.Interfaces
{
    public interface IUserService
    {
        User FindById(long id);
        User FindByEmail(String email);
        List<User> GetAll();
        List<User> GetAllExceptFor(User currentUser);
        void Add(User user);
        void DeleteById(long id);
        void Update();
        bool IsNewUser(string email);
        bool HasUserSameLevel(long levelId, User user);
        bool HasPitchForSameLevel(long levelId, User user);
    }
}
