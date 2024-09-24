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

       /* List<Order_Item> IOrderItemRepository.GetAll()
        {
            throw new NotImplementedException();
        }*/
    }
}