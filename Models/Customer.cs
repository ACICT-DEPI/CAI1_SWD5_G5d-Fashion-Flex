namespace Fashion_Flex.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone_Number { get; set; }
        public string Phone_Country_Code { get; set; }
        public string Address { get; set; }
        public DateOnly Date_Of_Birth { get; set; }
        public DateTime Account_Creation_Date { get; set; }
        public bool Is_Active { get; set; }

        //Customer |1--Places-->OM Order 
        public virtual List<Order>? Orders { get; set; }
        //Customer |1--Writes-->OM Review
        public virtual List<Review>? Reviews { get; set; }
        //Customer |1--Favorites-->OM Favorite_Brand
        public virtual List<Favorite_Brand>? Favorite_Brands { get; set; }
    }
}
