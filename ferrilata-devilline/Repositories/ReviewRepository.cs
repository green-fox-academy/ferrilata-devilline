using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ferrilata_devilline.Models.DAOs;
using Microsoft.EntityFrameworkCore;

namespace ferrilata_devilline.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationContext _applicationContext;

        public ReviewRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public List<Review> RetrieveReviewsFromDB()
        {
            return _applicationContext.Reviews
                .Include(review => review.User)
                .ThenInclude(user => user.UserLevels)
                .ThenInclude(userLevel => userLevel.Level)
                .ThenInclude(level => level.Badge)
                .Include(review => review.Pitch)
                .ThenInclude(pitch => pitch.Level)
                .ThenInclude(level => level.Badge)
                .ToList();
        }

        public void Delete(Review review)
        {
            _applicationContext.Remove(review);
        }

        public Review FindReviewById(long id)
        {
            Review review = RetrieveReviewsFromDB().SingleOrDefault(x => x.ReviewId == id);
            return review;
        }

        public void SaveReview(Review review)
        {
           _applicationContext.Add(review);
            _applicationContext.SaveChanges();
        }

        public void Update()
        {
            _applicationContext.SaveChanges();
        }
    }
}
