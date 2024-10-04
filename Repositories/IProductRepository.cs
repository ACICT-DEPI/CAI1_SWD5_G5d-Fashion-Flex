﻿using Fashion_Flex.Models;
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
		PaginatedList<Product> GetAllPaginated(int pageIndex, int pageSize, string sortOrder);
	}
}