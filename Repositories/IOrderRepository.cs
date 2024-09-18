using Fashion_Flex.Models;

namespace Fashion_Flex.Repository
{
    public interface IOrderRepository
    {
        public void Add(Order obj);
        public void Update(Order obj);
        public void Delete(int id);
        public Order GetById(int id);
        public List<Order> GetAll();
        void Save();
        Task<bool> DoCheckout(Order_Item model);
    }
}
