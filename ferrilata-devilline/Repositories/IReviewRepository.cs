using ferrilata_devilline.Models.DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ferrilata_devilline.Repositories
{
    public interface IReviewRepository
    {
        void SaveReview(Review review);
        List<Review> RetrieveReviewsFromDB();
        Review FindReviewById(long id);
        void Update();
        void Delete(Review review);
    }
}
