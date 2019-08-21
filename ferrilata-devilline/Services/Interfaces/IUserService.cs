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
        List<User> GetAll();
        void Add(User user);
        void DeleteById(long id);
        void Update();
    }
}
