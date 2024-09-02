using System.ComponentModel.DataAnnotations.Schema;

namespace Fashion_Flex.Models
{
	public class Favorite_Brand
	{
		public int Id { get; set; }

		//Relations
		[ForeignKey("Customer")]
		public int? Customer_Id { get; set; }

        [ForeignKey("Brand")]
        public int? Brand_Id { get; set; }

		public virtual Customer Customer { get; set; }
        public virtual Brand Brand { get; set; }
    }
}
