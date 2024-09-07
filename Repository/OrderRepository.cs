using Fashion_Flex.Models;

namespace Fashion_Flex.Repository
{
    public class OrderRepository
    {
        private readonly FFContext context;

        public OrderRepository(FFContext context)
        {
            this.context = context;
        }

        public void Add(Order obj)
        {
            context.Orders.Add(obj);
            //context.Add(obj);
        }
        public void Update(Order obj)
        {
            context.Orders.Update(obj);
        }
        public void Delete(int id)
        {
            var dept = GetById(id);
            context.Orders.Remove(dept);
        }
        public Order GetById(int id)
        {
            return context.Orders.FirstOrDefault(d => d.Id == id);
        }
        public List<Order> GetAll()
        {
            return context.Orders.ToList();
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
