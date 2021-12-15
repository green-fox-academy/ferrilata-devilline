using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Repositories;
using ferrilata_devilline.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ferrilata_devilline.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public List<Review> GetAll()
        {
            List<Review> reviews = _reviewRepository.RetrieveReviewsFromDB();
            return reviews;
        }

        public void Delete(Review review)
        {
            _reviewRepository.Delete(review);
        }

        public Review FindReviewById(long id)
        {
            Review review = GetAll().FirstOrDefault(x => x.ReviewId == id);
            return review;
        }

        public void Update()
        {
            _reviewRepository.Update();
        }

        public void Add(Review review)
        {
            _reviewRepository.SaveReview(review);
        }
    }
}
