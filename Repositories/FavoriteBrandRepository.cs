using Fashion_Flex.Models;

namespace Fashion_Flex.Repositories
{
    public class FavoriteBrandRepository : IFavoriteBrandRepository
    {
        private readonly FFContext context;
        public FavoriteBrandRepository(FFContext context)
        {
            this.context = context;
        }
        public void Add(Favorite_Brand newFavBrand)
        {
            context.Favorite_Brands.Add(newFavBrand);
        }
        public void Update(Favorite_Brand Favourite_Brand)
        {
            context.Favorite_Brands.Update(Favourite_Brand);
        }
        public void Delete(int Favourite_BrandId)
        {
            var Favourite_Brand = GetById(Favourite_BrandId);
            context.Favorite_Brands.Remove(Favourite_Brand);
        }
        public Favorite_Brand GetById(int Favourite_BrandId)
        {
            return context.Favorite_Brands.FirstOrDefault(F => F.Id == Favourite_BrandId);
        }
        public List<Favorite_Brand> GetAll()
        {
            return context.Favorite_Brands.ToList();
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
