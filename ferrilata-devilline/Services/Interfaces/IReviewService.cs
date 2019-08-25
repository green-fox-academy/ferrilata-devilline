using ferrilata_devilline.Models.DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ferrilata_devilline.Services.Interfaces
{
    public interface IReviewService
    {
        Review FindReviewById(long id);
        List<Review> GetAll();
        void Delete(Review review);
        void Add(Review review);
        void Update();
    }
}
