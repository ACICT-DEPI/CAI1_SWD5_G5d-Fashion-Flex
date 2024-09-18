using Fashion_Flex.Models;

namespace Fashion_Flex.Repository
{
    public interface IOrderItemRepository
    {
        Task<int> AddItem(int productID, int qty);
        Task<int> RemoveItem(int productID);
        /*Task<Order_Item> GetUserCart();
        Task<int> GetCartItemCount(int userId);
        Task<Order_Item> GetCart(int userId);*/

        /*public void Add(Order_Item obj);
        public void Update(Order obj);
        public void Delete(int id);
        public Order GetById(int id);
        public List<Order> GetAll();
        public void Save();
        void Update(Order_Item orderItem);*/
        //Task<bool> DoCheckout(Order_Item model);
    }
}
