using Fashion_Flex.Models;

namespace Fashion_Flex.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly FFContext context;
        public BrandRepository(FFContext context)
        {
            this.context = context;
        }

        public void Add(Brand newBrand)
        {
            context.Brands.Add(newBrand);
        }

        public void Update(Brand brand)
		{
			context.Brands.Update(brand);
		}
        public void Delete(int BrandId)
        {
            var brand = GetById(BrandId);
            context.Brands.Remove(brand);
        }
        public Brand GetById(int BrandId)
        {
            return context.Brands.FirstOrDefault(B => B.Id == BrandId);
        }

        public List<Brand> GetAll()
        {
            return context.Brands.ToList();
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
