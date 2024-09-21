namespace Fashion_Flex.Models
{
    public class User_Favorites
    {
        
            public int Id { get; set; }

        public User_Favorites(int id)
        {
            Id = id;
        }

        public int Customer_Id { get; set; }
            public int Product_Id { get; set; }
        
    }
}
