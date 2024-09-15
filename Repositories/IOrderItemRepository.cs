using Fashion_Flex.Models;

namespace Fashion_Flex.Repository
{
    public interface IOrderItemRepository
    {
        public void Add(Order_Item obj);
        public void Update(Order_Item obj);
        public void Delete(int id);
        public Order GetById(int id);
        public List<Order_Item> GetAll();
        public void Save();
    }
}
