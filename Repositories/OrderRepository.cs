using Fashion_Flex.Models;
using Fashion_Flex.Repository;
using Microsoft.EntityFrameworkCore;

namespace Fashion_Flex.IRepositories.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly FFContext context;

        public OrderRepository(FFContext context)
        {
            this.context = context;
        }

        public void Add(Order obj)
        {
            context.Orders.Add(obj);
        }
        public void Update(Order obj)
        {
            context.Orders.Update(obj);
        }
        public void Delete(int id)
        {
            var dept = GetOrderById(id);
            context.Orders.Remove(dept);
        }



        //Get Order
        public Order GetOrderById(int orderId)
        {
            return context.Orders.FirstOrDefault(d => d.Id == orderId);
        }

        public List<Order> GetOrdersByCustomerId(int customerId)
        {
            return context.Orders
                                 .Where(o => o.Customer_Id == customerId)
                                 .Include(o => o.Order_Items) // Optionally include OrderItems
                                 .ToList();
        }

        public List<Order> GetAll()
        {
            return context.Orders.ToList();
        }


        public void updateOrderStates(int orderId)
        {
            var order = GetOrderById(orderId);
            if (order != null)
            {

                order.Order_Status = "Complete";
                order.Order_Date = DateTime.Now;
                //order.Tracking_Number = ;
            }
            else
            {
                throw new InvalidOperationException($"order with id:{orderId} is not found");
            }
            Save();
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
