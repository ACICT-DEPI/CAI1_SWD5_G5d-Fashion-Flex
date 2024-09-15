using Fashion_Flex.Models;
using Microsoft.EntityFrameworkCore;

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

        public void Save()
        {
            context.SaveChanges();
        }

        public async Task<Customer> GetByUserIdAsync(string userId)
        {
            return await context.Customers.FirstOrDefaultAsync(c => c.ApplicationUserId == userId);
        }
    }
}
