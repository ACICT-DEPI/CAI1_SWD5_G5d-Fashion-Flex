using Fashion_Flex.Models;

namespace Fashion_Flex.Repositories
{
    public interface IBrandRepository
    {
        public void Add(Brand newBrand);
        public void Update(Brand Brand);
        public void Delete(int BrandId);
        public Brand GetById(int BrandId);
        public List<Brand> GetAll();
        public void Save();
    }
}
