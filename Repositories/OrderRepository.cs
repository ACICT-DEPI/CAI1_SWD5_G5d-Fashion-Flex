using Fashion_Flex.Models;
using Fashion_Flex.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Fashion_Flex.IRepositories.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly FFContext context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IdentityUser> _userManager;

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
        public async Task<Order> GetUserCart()
        {
            var userId = GetUserId();
            if (userId == null)
            {
                throw new Exception("Invalid userid");
            }
            var shoppingCart = await context.Orders.Where(a => a.Customer_Id == userId).FirstOrDefaultAsync();
            return shoppingCart;

        }
        public async Task<Order> GetCart(int userId)
        {
            var cart = await context.Orders.FirstOrDefaultAsync(x => x.Customer_Id == userId);
            return cart;
        }
        public async Task<int> GetCartItemCount(int userId)
        {
            if (string.IsNullOrEmpty(userId.ToString())) // updated line
            {
                userId = GetUserId();
            }
            var data = await (from cart in context.Orders
                              join cartDetail in context.Orders
                              on cart.Id equals cartDetail.Id
                              where cart.Customer_Id == userId // updated line
                              select new { cartDetail.Id }
                        ).ToListAsync();
            return data.Count;
        }
        private int GetUserId()
        {
            var principal = _httpContextAccessor.HttpContext.User;
            int userId = Convert.ToInt32(_userManager.GetUserId(principal));
            return userId;
        }
        public async Task<bool> DoCheckout(Order_Item model)
        {
            using var transaction = context.Database.BeginTransaction();
            try
            {
                // move data from OrderItem to order

                //Get User ID
                var userId = GetUserId();

                //Get Usser Cart
                var cart = await GetCart(userId);
                if (cart is null)
                {
                    throw new Exception("Invalid cart");
                }  

                //check if cart is empty
                var cartDetail = context.Orders
                                    .Where(a => a.Id == cart.Id).ToList();
                if (cartDetail.Count == 0)
                {
                    throw new Exception("Cart is empty");
                }

                //check if there are pending items 
                var pendingRecord = context.Orders.FirstOrDefault(s => s.Order_Status == "Pending");
                if (pendingRecord is null)
                {
                    throw new Exception("Order status does not have Pending status");
                }  
                
                // moving data to order
                var customer = new Customer
                {
                    First_Name = model.Order.Customer.First_Name,
                    Email = model.Order.Customer.Email,
                    Phone_Number = model.Order.Customer.Phone_Number,
                    Address = model.Order.Customer.Address,
                };
                var payment = new Payment
                {
                    Payment_Method = model.Order.Payment.Payment_Method,
                };
                var order = new Order
                {
                    Customer_Id = userId,
                    Order_Date = DateTime.UtcNow,
                    Customer = customer,
                    Payment = payment,
                    Id = pendingRecord.Id
                };
                context.Orders.Add(order);
                context.SaveChanges();

                /*// removing the cartdetails
                context.Order_Items.RemoveRange(cartDetail);
                context.SaveChanges();*/
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
