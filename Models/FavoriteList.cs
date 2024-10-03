using System.ComponentModel.DataAnnotations.Schema;

namespace Fashion_Flex.Models
{
	public class FavoriteList
	{
		public int Id { get; set; }
		[ForeignKey("Customer")]
		public int Customer_Id { get; set; }
		[ForeignKey("Product")]
		public int Product_Id { get; set; }

		//Customer |1--Has-->OM FavoriteList
		public virtual Customer Customer { get; set; }
		//Product |1--Favoried-->OM FavoriteList
		public virtual Product Product { get; set; }
	}
}
