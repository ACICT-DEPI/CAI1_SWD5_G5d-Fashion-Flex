using Fashion_Flex.Models;

namespace Fashion_Flex.Repository
{
    public interface ICustomerRepository
    {
        public void Add(Customer newCustomer);
        public Task<Customer> GetByUserIdAsync(string userId);
        public void Update(Customer Customer);
        public void Delete(int CustomerId);
        public Customer GetById(int CustomerId);
        public Customer GetByApplicationUserId(string ApplicationUserId);
        public List<Customer> GetAll();
        public void Save();

    }
}
