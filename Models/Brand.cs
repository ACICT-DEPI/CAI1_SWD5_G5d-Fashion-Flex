namespace Fashion_Flex.Models
{
	public class Brand
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int Logo { get; set; }

        //Relations
        public virtual List<Favorite_Brand> FavoriteBrands { get; set; }
    }
}
