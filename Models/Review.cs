using System.ComponentModel.DataAnnotations.Schema;

namespace Fashion_Flex.Models
{
	public class Review
	{
		public int Id { get; set; }
		public int Rating { get; set; }
		public string Comments { get; set; }
		public DateTime Review_Date { get; set; }
		public string is_verified_purchase { get; set; }

		// Relations
		[ForeignKey("Customer")]
        public int Customer_Id { get; set; }
        public virtual Customer Customer { get; set; }

        [ForeignKey("Product")]
        public int Product_Id { get; set; }
        public virtual Product Product { get; set; }
    }
}
