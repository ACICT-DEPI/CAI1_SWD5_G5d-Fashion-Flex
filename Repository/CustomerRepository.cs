using Fashion_Flex.Models;

namespace Fashion_Flex.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly FFContext context;
        public CustomerRepository(FFContext context)
        {
            this.context = context;
        }
        public void Add(Customer newCustomer)
        {
            context.Customers.Add(newCustomer);
        }

        public void Delete(int CustomerId)
        {
            var Customer = GetById(CustomerId);
            context.Customers.Remove(Customer);
        }

        public void Update(Customer Customer)
        {
            context.Customers.Update(Customer);
        }
        public List<Customer> GetAll()
        {
            return context.Customers.ToList();
        }

        public Customer GetById(int CustomerId)
        {
            return context.Customers.FirstOrDefault(c => c.Id == CustomerId);
        }

    }
}
