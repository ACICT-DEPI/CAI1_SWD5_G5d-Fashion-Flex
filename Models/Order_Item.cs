namespace Fashion_Flex.Models
{
	public class Order_Item
	{
		public int Id { get; set; }
		public int Order_Id { get; set; }
		public int Product_Id { get; set; }
		public int Quantity { get; set; }
	}
}
