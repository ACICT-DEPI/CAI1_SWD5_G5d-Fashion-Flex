using Fashion_Flex.Models;
using Fashion_Flex.Repository;
using Microsoft.AspNetCore.Identity;

namespace Fashion_Flex.IRepositories.Repository
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly FFContext context;

        public OrderItemRepository(FFContext context)
        {
            this.context = context;
        }

        public void Add(Order obj)
        {
            throw new NotImplementedException();
        }
        public void Add(Order_Item obj)
        {
            context.Order_Items.Add(obj);
        }

        public void Update(Order obj)
        {
            throw new NotImplementedException();
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

        Order IOrderItemRepository.GetById(int id)
        {
            throw new NotImplementedException();
        }
        public Order_Item GetById(int id)
        {
            return context.Order_Items.FirstOrDefault(d => d.Id == id);
        }

        List<Order> IOrderItemRepository.GetAll()
        {
            throw new NotImplementedException();
        }
        public List<Order_Item> GetAll()
        {
            return context.Order_Items.ToList();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private int GetUserId()
        {
            var principal = context.Customers;
            int userId = context.GetUserId(principal);
            return userId;
        }
        public async Task<Order_Item> GetCart(int userId)
        {

            var cart =  GetById(userId);
            return cart;
        }
        public async Task<bool> DoCheckout(Order model)
        {
            using var transaction = context.Database.BeginTransaction();
            try
            {
                // logic
                // move data from cartDetail to order and order detail then we will remove cart detail
                var userId = GetUserId();
                if (userId.ToString() == null)
                    throw new Exception("User is not logged-in");
                var cart = await GetCart((userId));
                if (cart is null)
                    throw new Exception("Invalid cart");
                var cartDetail = context.Products
                                    .Where(a => a.Id == cart.Id).ToList();
                if (cartDetail.Count == 0)
                    throw new Exception("Cart is empty");
                var pendingRecord = context.Order_Items.FirstOrDefault(s => s.Order_Status == "Pending");
                if (pendingRecord is null)
                    throw new Exception("Order status does not have Pending status");
                var customer = new Customer
                {
                    First_Name = model.Customer.First_Name,
                    Email = model.Customer.Email,
                    Phone_Number = model.Customer.Phone_Number,
                    Address = model.Customer.Address,


                };
                var payment = new Payment
                {
                    Payment_Method = model.Payment.Payment_Method,
                    Payment_Status = model.Payment.Payment_Status,
                };

                var order = new Order
                {
                    Customer_Id = userId,
                    Order_Date = DateTime.UtcNow,
                    Customer = customer,
                    Payment = payment,
                    Order_Status = pendingRecord.Order_Status
                };
                context.Orders.Add(order);
                context.SaveChanges();
                foreach (var item in cartDetail)
                {
                    var product = new Product
                    {
                        Price = item.Price
                    };
                    var orderDetail = new Order_Item
                    {
                        Product_Id = item.Id,
                        Order_Id = order.Id,
                        Quantity = item.Available_Quantity,
                        Product = product,
                    };
                    context.Order_Items.Add(orderDetail);
                }
                context.SaveChanges();

                // removing the cartdetails
                context.Remove(cartDetail);
                context.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

    }
}
