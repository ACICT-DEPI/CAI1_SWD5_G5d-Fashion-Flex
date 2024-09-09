using Fashion_Flex.Models;
using Fashion_Flex.Repository;
using Microsoft.EntityFrameworkCore;

namespace Fashion_Flex.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly FFContext context;
        public ReviewRepository(FFContext context)
        {
            this.context = context;
        }
        public void Add(Review newreview)
        {
            context.Reviews.Add(newreview);

        }
        public void Update(Review review)
        {
            context.Reviews.Update(review);
        }
        public void Delete(int reviewID)
        {
            var review = GetById(reviewID);
            context.Reviews.Remove(review);
        }
        public Review GetById(int reviewID)
        {
            return context.Reviews.FirstOrDefault(r => r.Id == reviewID);
        }
        public List<Review> GetAll()
        {
            return context.Reviews.ToList();
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}

