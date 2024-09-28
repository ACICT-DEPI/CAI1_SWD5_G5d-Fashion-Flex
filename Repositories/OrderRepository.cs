using Fashion_Flex.Models;
using Fashion_Flex.Repository;
using Microsoft.EntityFrameworkCore;

namespace Fashion_Flex.IRepositories.Repository
{
	public class OrderRepository : IOrderRepository
	{
		private readonly FFContext context;
        static Random rng = new Random();
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
			var order = GetOrderById(id);
			context.Orders.Remove(order);
		}

		//Get Order
		public Order GetOrderById(int orderId)
		{
			return context.Orders.FirstOrDefault(d => d.Id == orderId);
		}

		public List<Order> GetOrdersByCustomerId(int customerId)
		{
			return context.Orders.Where(o => o.Customer_Id == customerId)
								 .Include(o => o.Order_Items) // Optionally include OrderItems
								 .ToList();
		}

		public List<Order> GetAll()
		{
			return context.Orders.ToList();
		}

		public void Save()
		{
			throw new NotImplementedException();
		}
        /*public int generaterandomnum()
        {
            int min = 1;
            int max = 100;

            List<int> numbers = new List<int>();

            for (int i = min; i < max; i++)
            {
                numbers.Add(i);
            }

            // Shuffle the list
            Shuffle(numbers);
            return numbers;

        }


        static void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                int k = rng.Next(n--);
                T value = list[n];
                list[n] = list[k];
                list[k] = value;
            }
        }*/

        public void updateOrderStates(int orderid)
        {
            var Order = GetOrderById(orderid);
            if (Order != null)
            {

                Order.Order_Status = "Completed";
                Order.Order_Date = DateTime.Now;
                //Order.Tracking_Number = generaterandomnum();

            }
            else
            {
                throw new InvalidOperationException($"Order with id:{Order} is not found");
            }
        }

    }
}
