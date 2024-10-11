using Fashion_Flex.Models;
using Fashion_Flex.Repository;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Fashion_Flex.IRepositories.Repository
{
	public class OrderRepository : IOrderRepository
	{
		private static readonly Random random = new Random();


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
			var order = GetOrderById(id);
			context.Orders.Remove(order);
		}

		//Get Order
		public Order GetOrderById(int orderId)
		{
			return context.Orders
				.Include(o => o.Customer)
				.Include(o => o.Order_Items) // Include order items
				.ThenInclude(o => o.Product) // Include products in order items
				.FirstOrDefault(o => o.Id == orderId);
		}

		public List<Order> GetOrdersByCustomerId(int customerId)
		{
			return context.Orders.Where(o => o.Customer_Id == customerId)
								 .Include(o => o.Order_Items) // Optionally include OrderItems
								 .ToList();
		}

		public List<Order> GetAll()
		{

            return context.Orders
					.Include(o => o.Customer) 
					.ToList();
        }

		public void Save()
		{
			context.SaveChanges();
		}

		public bool updateOrderStatus(int orderid, string status)
		{
			var Order = GetOrderById(orderid);
			if (Order != null)
			{

				Order.Order_Status = status;
				Order.Tracking_Code = GenerateTrackingCode();

				Save();
				return true;
			}
			else
			{
				return false;
				throw new InvalidOperationException($"Order with id:{orderid} is not found");
			}
		}

		public Order GetCustomerCurrOrder(int customerId)
		{
			return context.Orders.Where(o => o.Order_Status == "Pending" && o.Customer_Id == customerId).FirstOrDefault();
		}


		private string GenerateTrackingCode()
		{
			int length = 6;

			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
			StringBuilder stringBuilder = new StringBuilder(length);

			for (int i = 0; i < length; i++)
			{
				// Directly append characters to StringBuilder
				stringBuilder.Append(chars[random.Next(chars.Length)]);
			}

			return stringBuilder.ToString(); // Convert to string at the end
		}
	}
}

