using Fashion_Flex.Models;

namespace Fashion_Flex.Repository
{
    public interface ICustomerRepository
    {
        public void Add(Customer newCustomer);
        public void Update(Customer Customer);
        public void Delete(int CustomerId);
        public Customer GetById(int CustomerId);
        public List<Customer> GetAll();
        public void Save();

    }
}
