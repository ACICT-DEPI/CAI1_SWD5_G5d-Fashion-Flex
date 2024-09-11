using Fashion_Flex.Models;

namespace Fashion_Flex.Repository
{
    public interface IReviewRepository
    {
        public void Add(Review newreview);
        public void Update(Review review);
        public void Delete(int reviewID);
        public Review GetById(int reviewID);
        public List<Review> GetAll();
        public void Save();

    }
}
