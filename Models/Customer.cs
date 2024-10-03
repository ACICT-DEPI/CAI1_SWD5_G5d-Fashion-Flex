using System.ComponentModel.DataAnnotations.Schema;

namespace Fashion_Flex.Models
{
	public class Customer
	{
		public int Id { get; set; }
		public string First_Name { get; set; }
		public string Last_Name { get; set; }
		public string Email { get; set; }
		public string? Phone_Number { get; set; }
		public string? Phone_Country_Code { get; set; }
		public string Address { get; set; }
		public DateOnly Date_Of_Birth { get; set; }
		public DateTime Account_Creation_Date { get; set; }
		public bool Is_Active { get; set; }

		// Foreign key to ApplicationUser
		[ForeignKey("ApplicationUser")]
		public string ApplicationUserId { get; set; }
		public ApplicationUser ApplicationUser { get; set; }

		//Customer |1--Places-->OM Order 
		public virtual List<Order>? Orders { get; set; }
		//Customer |1--Writes-->OM Review
		public virtual List<Review>? Reviews { get; set; }
		//Customer |1--Favorite-->OM User_Favorites
		public virtual List<FavoriteList>? User_Favorites { get; set; }
	}
}
