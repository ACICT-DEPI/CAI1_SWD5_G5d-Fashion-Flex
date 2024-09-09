using Fashion_Flex.Models;

namespace Fashion_Flex.Repository
{
    public interface IOrderItemRepository
    {
        public void Add(Order obj);
        public void Update(Order obj);
        public void Delete(int id);
        public Order GetById(int id);
        public List<Order> GetAll();
        public void Save();
    }
}
