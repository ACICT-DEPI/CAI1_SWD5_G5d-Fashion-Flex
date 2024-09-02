using System.ComponentModel.DataAnnotations.Schema;

namespace Fashion_Flex.Models
{
	public class Order_Item
	{
		public int Id { get; set; }
		
		public int Quantity { get; set; }

        //Relations
        [ForeignKey("Order")]
        public int Order_Id { get; set; }
        public virtual Order Order { get; set; }

        [ForeignKey("Product")]
        public int Product_Id { get; set; }
        public virtual Product Product { get; set; }
    }
}
