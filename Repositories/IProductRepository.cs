using Fashion_Flex.Models;
using Fashion_Flex.ViewModels;

namespace Fashion_Flex.IRepositories
{
	public interface IProductRepository
	{
		public void Add(Product newProduct);
		public void Update(Product Product);
		public void Delete(int ProductId);
		public Product GetById(int ProductId);
		public List<Product> GetAll();
		public void Save();
		public PaginatedList<Product> GetRefinedPages(int pageIndex, int pageSize, string sortOrder, string category, string type, string searchString);
		public IQueryable<Product> FilterByCategory(IQueryable<Product> products, string category);
		public IEnumerable<string> GetCategories();
		public IEnumerable<string> GetTypes();
	}
}