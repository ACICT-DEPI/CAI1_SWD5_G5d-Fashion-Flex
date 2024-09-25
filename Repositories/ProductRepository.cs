using Fashion_Flex.Models;
using Fashion_Flex.IRepositories;

namespace Fashion_Flex.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly FFContext context;

        public ProductRepository(FFContext context)
        {
            this.context = context;
        }

        public void Add(Product newProduct)
        {
            context.Products.Add(newProduct);
        }

        public void Delete(int ProductId)
        {
            var product = GetById(ProductId);
            if (product != null)
            {
                context.Products.Remove(product);
            }
        }

        public void Update(Product product)
        {
            context.Products.Update(product);
        }

        public List<Product> GetAll()
        {
            return context.Products.ToList();
            //.Include(p => p.Brand)
            //.Include(p => p.Order_Items)
            //.Include(p => p.Reviews)
            //.ToList();
        }

        public Product GetById(int ProductId)
        {
            return context.Products

                .FirstOrDefault(p => p.Id == ProductId);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
