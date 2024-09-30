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
            var item = GetById(id);
            context.Order_Items.Remove(item);
        }
        public Order_Item GetById(int id)
        {
            return context.Order_Items.FirstOrDefault(d => d.Id == id);
        }
        public List<Order_Item> GetAll()
        {
            return context.Order_Items.ToList();
        }
		public bool OrderItemExist(int orderId, int productId)
        {
            return context.Order_Items.Where(i => i.Order_Id==orderId && i.Product_Id==productId).Any();
        }
		public Order_Item GetByProductAndOrderId(int orderId, int productId)
        {
			var item = context.Order_Items.Where(i => i.Order_Id == orderId && i.Product_Id == productId).FirstOrDefault();
            return item;
		}
		public void Save()
        {
            context.SaveChanges();
        }
    }
}