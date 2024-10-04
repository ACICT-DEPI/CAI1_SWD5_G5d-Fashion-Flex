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

		public PaginatedList<Product> GetAllPaginated(int pageIndex, int pageSize, string sortOrder)
		{
			var products = from p in context.Products
						   select p;

			// Sorting logic based on sortOrder
			switch (sortOrder)
			{
				case "name_desc":
					products = products.OrderByDescending(p => p.Name);
					break;
				case "price":
					products = products.OrderBy(p => p.Price);
					break;
				case "price_desc":
					products = products.OrderByDescending(p => p.Price);
					break;
				case "date":
					products = products.OrderBy(p => p.Added_Date);
					break;
				case "newness":
					products = products.OrderByDescending(p => p.Added_Date);
					break;
				//case "popularity":
				//	products = products.OrderBy(p => p.Popularity); // Assuming you have a popularity field
				//	break;
				//case "average_rating":
				//	products = products.OrderByDescending(p => p.AverageRating); // Assuming a rating field
				//	break;
				default:
					products = products.OrderBy(p => p.Name); // Default is name ascending
					break;
			}

			var totalCount = products.Count();

			var items = products.Skip((pageIndex - 1) * pageSize)
								.Take(pageSize)
								.ToList();

			return new PaginatedList<Product>(items, totalCount, pageIndex, pageSize);
		}
	}
}
