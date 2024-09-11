using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace Fashion_Flex.Models
{
	public class Payment
	{
		public int Id { get; set; }
		
		public DateTime Payment_Date { get; set; }
		public string Payment_Method { get; set; }
		public decimal Amount { get; set; }
		public string Currency { get; set; }
		public string Payment_Status { get; set; }
		public int Transaction_Id { get; set; }

		//Relations
		[ForeignKey("Order")]
        public int Order_Id { get; set; }
        public virtual Order Order { get; set; }

    }
}
