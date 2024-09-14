using Fashion_Flex.Models;
using Fashion_Flex.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Fashion_Flex.IRepositories.Repository
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly FFContext context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IdentityUser> _userManager;

        public OrderItemRepository(FFContext context)
        {
            this.context = context;
        }

        public async Task<int> AddItem(int productID, int qty)
        {
            int userId = GetUserId();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                if (string.IsNullOrEmpty(userId.ToString()))
                    throw new Exception("user is not logged-in");
                var cart = await GetCart(userId);
                if (cart is null)
                {
                    Customer customer = new Customer
                    {
                        Id = userId
                    };
                    Order order = new Order
                    {
                        Customer = customer,
                    };
                    cart = new Order_Item
                    {
                        Order = order,
                    };
                    context.Order_Items.Add(cart);
                }
                context.SaveChanges();
                // cart detail section
                var cartItem = context.Order_Items
                                  .FirstOrDefault(a => a.Id == cart.Id && a.Order_Id == productID);
                if (cartItem is not null)
                {
                    cartItem.Quantity += qty;
                }
                else
                {
                    var product = context.Products.Find(productID);
                    var newproduct = new Product
                    {
                        Price = product.Price,
                    };
                    cartItem = new Order_Item
                    {
                        Product_Id = productID,
                        Id = cart.Id,
                        Quantity = qty,
                        Product = newproduct,
                    };
                    context.Order_Items.Add(cartItem);
                }
                context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
            }
            var cartItemCount = await GetCartItemCount(userId);
            return cartItemCount;
        }


        public async Task<int> RemoveItem(int productID)
        {
            //using var transaction = _db.Database.BeginTransaction();
            int userId = GetUserId();
            try
            {
                if (string.IsNullOrEmpty(userId.ToString()))
                    throw new Exception("user is not logged-in");
                var cart = await GetCart(userId);
                if (cart is null)
                    throw new Exception("Invalid cart");
                // cart detail section
                var cartItem = context.Order_Items
                                  .FirstOrDefault(a => a.Id == cart.Id && a.Product_Id == productID);
                if (cartItem is null)
                    throw new Exception("Not items in cart");
                else if (cartItem.Quantity == 1)
                    context.Order_Items.Remove(cartItem);
                else
                    cartItem.Quantity = cartItem.Quantity - 1;
                context.SaveChanges();
            }
            catch (Exception ex)
            {

            }
            var cartItemCount = await GetCartItemCount(userId);
            return cartItemCount;
        }

        public async Task<Order_Item> GetUserCart()
        {
            var userId = GetUserId();
            if (userId == null)
                throw new Exception("Invalid userid");
            var shoppingCart = await context.Order_Items.Where(a => a.Order.Customer_Id == userId).FirstOrDefaultAsync();
            return shoppingCart;

        }
        public async Task<Order_Item> GetCart(int userId)
        {
            var cart = await context.Order_Items.FirstOrDefaultAsync(x => x.Order.Customer_Id == userId);
            return cart;
        }

        public async Task<int> GetCartItemCount(int userId)
        {
            if (string.IsNullOrEmpty(userId.ToString())) // updated line
            {
                userId = GetUserId();
            }
            var data = await (from cart in context.Order_Items
                              join cartDetail in context.Order_Items
                              on cart.Id equals cartDetail.Id
                              where cart.Order.Customer_Id == userId // updated line
                              select new { cartDetail.Id }
                        ).ToListAsync();
            return data.Count;
        }

        public async Task<bool> DoCheckout(Order_Item model)
        {
            using var transaction = context.Database.BeginTransaction();
            try
            {
                // logic
                // move data from cartDetail to order and order detail then we will remove cart detail
                var userId = GetUserId();
                if (string.IsNullOrEmpty(userId.ToString()))
                    throw new Exception("User is not logged-in");
                var cart = await GetCart(userId);
                if (cart is null)
                    throw new Exception("Invalid cart");
                var cartDetail = context.Order_Items
                                    .Where(a => a.Id == cart.Id).ToList();
                if (cartDetail.Count == 0)
                    throw new Exception("Cart is empty");
                var pendingRecord = context.Order_Items.FirstOrDefault(s => s.Order_Status == "Pending");
                if (pendingRecord is null)
                    throw new Exception("Order status does not have Pending status");
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
                foreach (var item in cartDetail)
                {
                    var orderDetail = new Order_Item
                    {
                        Product_Id = item.Product_Id,
                        Order_Id = order.Id,
                        Quantity = item.Quantity,
                        Product = item.Product
                    };
                    context.Order_Items.Add(orderDetail);
                }
                context.SaveChanges();

                // removing the cartdetails
                context.Order_Items.RemoveRange(cartDetail);
                context.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        private int GetUserId()
        {
            var principal = _httpContextAccessor.HttpContext.User;
            int userId = Convert.ToInt32(_userManager.GetUserId(principal));
            return userId;
        }


    }
}