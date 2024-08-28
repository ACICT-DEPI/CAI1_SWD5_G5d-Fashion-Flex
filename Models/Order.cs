using System;
namespace Fashion_Flex.Models
{
	public class Order
	{
		public int Id { get; set; }
		public int Customer_Id { get; set; }			
		public DateTime Order_Date { get; set; }
		public int Total_Amount { get; set; }
		public string Order_Status { get; set; }
		public string Shipping_Address{ get; set; }
		public int Tracking_Number { get; set; }
	}
}
