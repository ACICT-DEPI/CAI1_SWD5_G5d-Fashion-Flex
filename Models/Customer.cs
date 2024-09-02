namespace Fashion_Flex.Models
{
	public class Customer
	{		
		public int Id { get; set; }
		public string First_Name { get; set; }
		public string Last_Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string Phone_Country_Code { get; set; }
		public int Phone_Number { get; set; }
		public string Address { get; set; }
		public DateTime Date_Of_Birth { get; set; }
		public DateTime Account_Creation_Date { get; set; }
		public string is_active { get; set; }		

		//Relations
		public virtual List<Favorite_Brand> FavoriteBrands { get; set; }
        public virtual List<Order> Orders { get; set; }

    }
}
