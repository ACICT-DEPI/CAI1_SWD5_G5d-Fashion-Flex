namespace Fashion_Flex.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }


        //Product OM--Own-->|1 Brand
        public virtual List<Product>? Products { get; set; }
        //Brand O1--Admire-->OM Favorite_Brand
        public virtual List<Favorite_Brand>? FavoriteBrands { get; set; }
    }
}
