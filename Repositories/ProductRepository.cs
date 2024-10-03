using Fashion_Flex.Models;
using Fashion_Flex.IRepositories;
using Microsoft.EntityFrameworkCore;
using Fashion_Flex.ViewModels;

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

		public PaginatedList<Product> GetPaginatedProducts(int pageIndex, int pageSize)
		{
			var totalCount = context.Products.Count();
			var products = context.Products
								   .OrderBy(p => p.Name) // Adjust sorting as necessary
								   .Skip((pageIndex - 1) * pageSize)
								   .Take(pageSize)
								   .ToList();

			return new PaginatedList<Product>(products, totalCount, pageIndex, pageSize);
		}
	}
}
