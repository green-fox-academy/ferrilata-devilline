using ferrilata_devilline.Models.DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ferrilata_devilline.Repositories
{
    public interface IUserRepository
    {
        void Update();
        void SaveUser(User user);
        List<User> RetrieveUsersFromDB();
        User FindUserById(long id);
        void DeleteUsereById(long id);
    }
}
