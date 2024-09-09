using Fashion_Flex.Models;

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
    }
}
