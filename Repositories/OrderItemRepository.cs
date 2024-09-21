using Fashion_Flex.Models;
using Fashion_Flex.Repository;

namespace Fashion_Flex.IRepositories.Repository
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly FFContext context;

        public OrderItemRepository(FFContext context)
        {
            this.context = context;
        }

        public void Add(Order_Item obj)
        {
            context.Order_Items.Add(obj);
        }
        public void Update(Order_Item obj)
        {
            context.Order_Items.Update(obj);
        }
        public void Delete(int id)
        {
            var dept = GetById(id);
            context.Order_Items.Remove(dept);
        }
        public Order_Item GetById(int id)
        {
            return context.Order_Items.FirstOrDefault(d => d.Id == id);
        }
        public List<Order_Item> GetAll()
        {
            return context.Order_Items.ToList();
        }
        public void Save()
        {
            context.SaveChanges();
        }

        Order_Item IOrderItemRepository.GetById(int id)
        {
            throw new NotImplementedException();
        }

        List<Order_Item> IOrderItemRepository.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
