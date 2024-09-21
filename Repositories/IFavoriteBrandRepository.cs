using Fashion_Flex.Models;

namespace Fashion_Flex.Repositories
{
    public interface IFavoriteBrandRepository
    {
        public void Add(Favorite_Brand newFavBrand);
        public void Update(Favorite_Brand FavBrand);
        public void Delete(int FavBrandId);
        public Favorite_Brand GetById(int FavBrandId);
        public List<Favorite_Brand> GetAll();
        public void Save();
    }
}
